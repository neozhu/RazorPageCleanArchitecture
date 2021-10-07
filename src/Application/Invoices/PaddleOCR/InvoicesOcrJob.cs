// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Hubs;
using CleanArchitecture.Razor.Application.Hubs.Constants;
using CleanArchitecture.Razor.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitecture.Razor.Application.Invoices.PaddleOCR
{
    public class InvoicesOcrJob : IInvoicesOcrJob
    {
        private readonly IHubContext<SignalRHub> _hubContext;
        private readonly IApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;


        public InvoicesOcrJob(
            IHubContext<SignalRHub> hubContext,
            IApplicationDbContext context,
            IHttpClientFactory httpClientFactory
            )
        {
            _hubContext = hubContext;
            _context = context;
            _httpClientFactory = httpClientFactory;

        }

        public void Recognition(int id)
        {
            using (var client = _httpClientFactory.CreateClient("ocr"))
            {
                var invoice = _context.Invoices.Find(id);
                var imgfile = Path.Combine(Directory.GetCurrentDirectory(), invoice.AttachmentUrl);
                var bytes = File.ReadAllBytes(imgfile);
                string base64string = Convert.ToBase64String(bytes);
                var response = client.PostAsJsonAsync<dynamic>("", new { images = new string[] { base64string } }).Result;
                if(response.StatusCode== System.Net.HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var ocr_result = JsonSerializer.Deserialize<ocr_result>(result);
                    var ocr_status = "";
                    invoice.Status = "Done";
                    invoice.Result = ocr_result.status;
                    if (ocr_result.status== "000")
                    {
                        foreach(var collection in ocr_result.results)
                        {
                            foreach(var item in collection)
                            {
                                var rawdata = new InvoiceRawData()
                                {
                                     Confidence=item.confidence,
                                     InvoiceId=id,
                                     Text=item.text,
                                     Text_Region= JsonSerializer.Serialize(item.text_region)
                                };
                                if (item.text.Contains("发票号码"))
                                {
                                    var regex = new Regex("\\d*$");
                                    var mc = regex.Match(item.text);
                                    if(mc.Success)
                                    {
                                        invoice.InvoiceNo = mc.Value;
                                    }
                                }
                                if (item.text.Contains("开票日期"))
                                {
                                    var regex = new Regex("\\d{4}年\\d{2}月\\d{2}日");
                                    var mc = regex.Match(item.text);
                                    if (mc.Success)
                                    {
                                        invoice.InvoiceDate = Convert.ToDateTime(mc.Value.Replace("年","/").Replace("月", "/").Replace("日", ""));
                                    }
                                }
                                if (item.text.Contains("%"))
                                {
                                    var regex = new Regex("^\\d*.\\d*");
                                    var mc = regex.Match(item.text);
                                    if (mc.Success)
                                    {
                                        invoice.TaxRate = decimal.Parse(mc.Value);
                                    }
                                }
                                if (item.text.Contains("￥"))
                                {
                                    var regex = new Regex("\\d.\\d*");
                                    var mc = regex.Match(item.text);
                                    if (mc.Success)
                                    {
                                        invoice.Amount = decimal.Parse(mc.Value);
                                    }
                                }
                                _context.InvoiceRawDatas.Add(rawdata);
                            }
                        }
                        ocr_status = ocr_result.status;
                       
                    }
                    _context.SaveChangesAsync(default).Wait();
                    _hubContext.Clients.All.SendAsync(SignalR.OCRTaskCompleted, new { invoiceNo = invoice.InvoiceNo  });



                }

            }
            Console.WriteLine($"{id}, completed.");
        }
    }

     class result
    {
        public decimal confidence { get; set; }
        public string text { get; set; }
        public List<int[]> text_region { get; set; }
    }
     class ocr_result
    {
        public string msg { get; set; }
        public List<result[]> results { get; set; }
        public string status { get; set; }
    }
    

}
