// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Domain.Entities;

namespace CleanArchitecture.Razor.Application.Invoices.PaddleOCR
{
    public class OcrJob : IOcrJob
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;


        public OcrJob(
            IApplicationDbContext context,
            IHttpClientFactory httpClientFactory
            )
        {
            _context = context;
            _httpClientFactory = httpClientFactory;

        }

        public void Recognition(int id, string base64string)
        {
            using (var client = _httpClientFactory.CreateClient("ocr"))
            {
                var response = client.PostAsJsonAsync<dynamic>("", new { images = new string[] { base64string } }).Result;
                if(response.StatusCode== System.Net.HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var ocr_result = JsonSerializer.Deserialize<ocr_result>(result);
                    var ocr_status = "";
                    var invoice = _context.Invoices.Find(id);
                    invoice.Status = "Completed";
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
                                _context.InvoiceRawDatas.Add(rawdata);

                            }
                        }
                        ocr_status = ocr_result.status;
                       
                    }
                    _context.SaveChangesAsync(default).Wait();
                 
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
