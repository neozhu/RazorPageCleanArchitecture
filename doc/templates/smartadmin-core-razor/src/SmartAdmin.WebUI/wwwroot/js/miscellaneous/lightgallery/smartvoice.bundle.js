/*
 * VOICE COMMAND CONFIG
 * dependency: js/speech/voicecommand.js
 */
	voice_command = true,
/*
 * Turns on speech as soon as the page is loaded
 */	
	voice_command_auto = false,
/*
 * 	Sets the language to the default 'en-US'. (supports over 50 languages 
 * 	by google)
 * 
 *  Afrikaans         ['af-ZA']
 *  Bahasa Indonesia  ['id-ID']
 *  Bahasa Melayu     ['ms-MY']
 *  Català            ['ca-ES']
 *  Čeština           ['cs-CZ']
 *  Deutsch           ['de-DE']
 *  English           ['en-AU', 'Australia']
 *                    ['en-CA', 'Canada']
 *                    ['en-IN', 'India']
 *                    ['en-NZ', 'New Zealand']
 *                    ['en-ZA', 'South Africa']
 *                    ['en-GB', 'United Kingdom']
 *                    ['en-US', 'United States']
 *  Español           ['es-AR', 'Argentina']
 *                    ['es-BO', 'Bolivia']
 *                    ['es-CL', 'Chile']
 *                    ['es-CO', 'Colombia']
 *                    ['es-CR', 'Costa Rica']
 *                    ['es-EC', 'Ecuador']
 *                    ['es-SV', 'El Salvador']
 *                    ['es-ES', 'España']
 *                    ['es-US', 'Estados Unidos']
 *                    ['es-GT', 'Guatemala']
 *                    ['es-HN', 'Honduras']
 *                    ['es-MX', 'México']
 *                    ['es-NI', 'Nicaragua']
 *                    ['es-PA', 'Panamá']
 *                    ['es-PY', 'Paraguay']
 *                    ['es-PE', 'Perú']
 *                    ['es-PR', 'Puerto Rico']
 *                    ['es-DO', 'República Dominicana']
 *                    ['es-UY', 'Uruguay']
 *                    ['es-VE', 'Venezuela']
 *  Euskara           ['eu-ES']
 *  Français          ['fr-FR']
 *  Galego            ['gl-ES']
 *  Hrvatski          ['hr_HR']
 *  IsiZulu           ['zu-ZA']
 *  Íslenska          ['is-IS']
 *  Italiano          ['it-IT', 'Italia']
 *                    ['it-CH', 'Svizzera']
 *  Magyar            ['hu-HU']
 *  Nederlands        ['nl-NL']
 *  Norsk bokmål      ['nb-NO']
 *  Polski            ['pl-PL']
 *  Português         ['pt-BR', 'Brasil']
 *                    ['pt-PT', 'Portugal']
 *  Română            ['ro-RO']
 *  Slovenčina        ['sk-SK']
 *  Suomi             ['fi-FI']
 *  Svenska           ['sv-SE']
 *  Türkçe            ['tr-TR']
 *  български         ['bg-BG']
 *  Pусский           ['ru-RU']
 *  Српски            ['sr-RS']
 *  한국어          ['ko-KR']
 *  中文                            ['cmn-Hans-CN', '普通话 (中国大陆)']
 *                    ['cmn-Hans-HK', '普通话 (香港)']
 *                    ['cmn-Hant-TW', '中文 (台灣)']
 *                    ['yue-Hant-HK', '粵語 (香港)']
 *  日本語                         ['ja-JP']
 *  Lingua latīna     ['la']
 */
	voice_command_lang = 'en-US',
/*
 * 	Use localstorage to remember on/off (best used with HTML Version
 * 	when going from one page to the next)
 */	
	voice_localStorage = false;
/*
 * Voice Commands
 * Defines voice command variables and functions
 */	
 	if (voice_command) {
	 		
		var commands = {
					
			'show dashboard' : function() { $('nav a[href="ajax/dashboard.html"]').trigger("click"); },
			'show inbox' : function() { $('nav a[href="ajax/inbox.html"]').trigger("click"); },
			'show graphs' : function() { $('nav a[href="ajax/flot.html"]').trigger("click"); },
			'show flotchart' : function() { $('nav a[href="ajax/flot.html"]').trigger("click"); },
			'show morris chart' : function() { $('nav a[href="ajax/morris.html"]').trigger("click"); },
			'show inline chart' : function() { $('nav a[href="ajax/inline-charts.html"]').trigger("click"); },
			'show dygraphs' : function() { $('nav a[href="ajax/dygraphs.html"]').trigger("click"); },
			'show tables' : function() { $('nav a[href="ajax/table.html"]').trigger("click"); },
			'show data table' : function() { $('nav a[href="ajax/datatables.html"]').trigger("click"); },
			'show jquery grid' : function() { $('nav a[href="ajax/jqgrid.html"]').trigger("click"); },
			'show form' : function() { $('nav a[href="ajax/form-elements.html"]').trigger("click"); },
			'show form layouts' : function() { $('nav a[href="ajax/form-templates.html"]').trigger("click"); },
			'show form validation' : function() { $('nav a[href="ajax/validation.html"]').trigger("click"); },
			'show form elements' : function() { $('nav a[href="ajax/bootstrap-forms.html"]').trigger("click"); },
			'show form plugins' : function() { $('nav a[href="ajax/plugins.html"]').trigger("click"); },
			'show form wizards' : function() { $('nav a[href="ajax/wizards.html"]').trigger("click"); },
			'show bootstrap editor' : function() { $('nav a[href="ajax/other-editors.html"]').trigger("click"); },
			'show dropzone' : function() { $('nav a[href="ajax/dropzone.html"]').trigger("click"); },
			'show image cropping' : function() { $('nav a[href="ajax/image-editor.html"]').trigger("click"); },
			'show general elements' : function() { $('nav a[href="ajax/general-elements.html"]').trigger("click"); },
			'show buttons' : function() { $('nav a[href="ajax/buttons.html"]').trigger("click"); },
			'show fontawesome' : function() { $('nav a[href="ajax/fa.html"]').trigger("click"); },
			'show glyph icons' : function() { $('nav a[href="ajax/glyph.html"]').trigger("click"); },
			'show flags' : function() { $('nav a[href="ajax/flags.html"]').trigger("click"); },
			'show grid' : function() { $('nav a[href="ajax/grid.html"]').trigger("click"); },
			'show tree view' : function() { $('nav a[href="ajax/treeview.html"]').trigger("click"); },
			'show nestable lists' : function() { $('nav a[href="ajax/nestable-list.html"]').trigger("click"); },
			'show jquery U I' : function() { $('nav a[href="ajax/jqui.html"]').trigger("click"); },
			'show typography' : function() { $('nav a[href="ajax/typography.html"]').trigger("click"); },
			'show calendar' : function() { $('nav a[href="ajax/calendar.html"]').trigger("click"); },
			'show widgets' : function() { $('nav a[href="ajax/widgets.html"]').trigger("click"); },
			'show gallery' : function() { $('nav a[href="ajax/gallery.html"]').trigger("click"); },
			'show maps' : function() { $('nav a[href="ajax/gmap-xml.html"]').trigger("click"); },
			'show pricing tables' : function() { $('nav a[href="ajax/pricing-table.html"]').trigger("click"); },
			'show invoice' : function() { $('nav a[href="ajax/invoice.html"]').trigger("click"); },
			'show search' : function() { $('nav a[href="ajax/search.html"]').trigger("click"); },
			'go back' :  function() { history.back(1); }, 
			'scroll up' : function () { $('html, body').animate({ scrollTop: 0 }, 100); },
			'scroll down' : function () { $('html, body').animate({ scrollTop: $(document).height() }, 100);},
			'hide navigation' : function() { 
				if ($.root_.hasClass("container") && !$.root_.hasClass("menu-on-top")){
					$('span.minifyme').trigger("click");
				} else {
					$('#hide-menu > span > a').trigger("click"); 
				}
			},
			'show navigation' : function() { 
				if ($.root_.hasClass("container") && !$.root_.hasClass("menu-on-top")){
					$('span.minifyme').trigger("click");
				} else {
					$('#hide-menu > span > a').trigger("click"); 
				}
			},
			'mute' : function() {
				$.sound_on = false;
				$.smallBox({
					title : "MUTE",
					content : "All sounds have been muted!",
					color : "#a90329",
					timeout: 4000,
					icon : "fa fa-volume-off"
				});
			},
			'sound on' : function() {
				$.sound_on = true;
				$.speechApp.playConfirmation();
				$.smallBox({
					title : "UNMUTE",
					content : "All sounds have been turned on!",
					color : "#40ac2b",
					sound_file: 'voice_alert',
					timeout: 5000,
					icon : "fa fa-volume-up"
				});
			},
			'stop' : function() {
				smartSpeechRecognition.abort();
				$.root_.removeClass("voice-command-active");
				$.smallBox({
					title : "VOICE COMMAND OFF",
					content : "Your voice commands has been successfully turned off. Click on the <i class='fa fa-microphone fa-lg fa-fw'></i> icon to turn it back on.",
					color : "#40ac2b",
					sound_file: 'voice_off',
					timeout: 8000,
					icon : "fa fa-microphone-slash"
				});
				if ($('#speech-btn .popover').is(':visible')) {
					$('#speech-btn .popover').fadeOut(250);
				}
			},
			'help' : function() {
				$('#voiceModal').removeData('modal').modal( { remote: "ajax/modal-content/modal-voicecommand.html", show: true } );
				if ($('#speech-btn .popover').is(':visible')) {
					$('#speech-btn .popover').fadeOut(250);
				}
			},		
			'got it' : function() {
				$('#voiceModal').modal('hide');
			},	
			'logout' : function() {
				$.speechApp.stop();
				window.location = $('#logout > span > a').attr("href");
			}
		}; 
		
	};
/*
 * SMART VOICE
 * Author: MyOrange | @bootstraphunt
 * http://www.myorange.ca
 */

SpeechRecognition = root.SpeechRecognition || root.webkitSpeechRecognition || root.mozSpeechRecognition || root.msSpeechRecognition || root.oSpeechRecognition;

// ref: http://updates.html5rocks.com/2013/01/Voice-Driven-Web-Apps-Introduction-to-the-Web-Speech-API
if (SpeechRecognition && voice_command) {

	// commands are pulled from app.config file

	// add function to button
	$.root_.on('click', '[data-action="voiceCommand"]', function(e) {

		if ($.root_.hasClass("voice-command-active")) {
			$.speechApp.stop();
			//$('#speech-btn > span > a > i').removeClass().addClass('fa fa-microphone-slash');
		} else {
			$.speechApp.start();
			//add popover
			$('#speech-btn .popover').fadeIn(350);
			//$('#speech-btn > span > a > i').removeClass().addClass('fa fa-microphone')

		}

		e.preventDefault();
	});

	//remove popover
	$(document).mouseup(function(e) {
		if (!$('#speech-btn .popover').is(e.target) && $('#speech-btn .popover').has(e.target).length === 0) {
			$('#speech-btn .popover').fadeOut(250);
		}
	});

	// create dynamic modal instance
	var modal = $('<div class="modal fade" id="voiceModal" tabindex="-1" role="dialog" aria-labelledby="remoteModalLabel" aria-hidden="true"><div class="modal-dialog"><div class="modal-content"></div></div></div>');
	// attach to body
	modal.appendTo("body");

	//debugState
	if (debugState) {
		root.console.log("This browser supports Voice Command");
	}

	// function
	$.speechApp = (function(speech) {

		speech.start = function() {

			// Add our commands to smartSpeechRecognition
			smartSpeechRecognition.addCommands(commands);

			if (smartSpeechRecognition) {
				// activate plugin
				smartSpeechRecognition.start();
				// add btn class
				$.root_.addClass("voice-command-active");
				// play sound
				$.speechApp.playON();
				// set localStorage when switch is on manually
				if (voice_localStorage) {
					localStorage.setItem('sm-setautovoice', 'true');
				}

			} else {
				// if plugin not found
				alert("speech plugin not loaded");
			}

		};
		speech.stop = function() {

			if (smartSpeechRecognition) {
				// deactivate plugin
				smartSpeechRecognition.abort();
				// remove btn class
				$.root_.removeClass("voice-command-active");
				// sound
				$.speechApp.playOFF();
				// del localStorage when switch if off manually
				if (voice_localStorage) {
					localStorage.setItem('sm-setautovoice', 'false');
				}
				// remove popover if visible
				if ($('#speech-btn .popover').is(':visible')) {
					$('#speech-btn .popover').fadeOut(250);
				}
			}

		};

		// play sound
		speech.playON = function() {

			var audioElement = document.createElement('audio');

			if (navigator.userAgent.match('Firefox/'))
				audioElement.setAttribute('src', $.sound_path + 'voice_on' + ".ogg");
			else
				audioElement.setAttribute('src', $.sound_path + 'voice_on' + ".mp3");

			//$.get();
			audioElement.addEventListener("load", function() {
				audioElement.play();
			}, true);

			if ($.sound_on) {
				audioElement.pause();
				audioElement.play();
			}
		};

		speech.playOFF = function() {

			var audioElement = document.createElement('audio');

			if (navigator.userAgent.match('Firefox/'))
				audioElement.setAttribute('src', $.sound_path + 'voice_off' + ".ogg");
			else
				audioElement.setAttribute('src', $.sound_path + 'voice_off' + ".mp3");

			$.get();
			audioElement.addEventListener("load", function() {
				audioElement.play();
			}, true);

			if ($.sound_on) {
				audioElement.pause();
				audioElement.play();
			}
		};

		speech.playConfirmation = function() {

			var audioElement = document.createElement('audio');

			if (navigator.userAgent.match('Firefox/'))
				audioElement.setAttribute('src', $.sound_path + 'voice_alert' + ".ogg");
			else
				audioElement.setAttribute('src', $.sound_path + 'voice_alert' + ".mp3");

			$.get();
			audioElement.addEventListener("load", function() {
				audioElement.play();
			}, true);

			if ($.sound_on) {
				audioElement.pause();
				audioElement.play();
			}
		};

		return speech;

	})({});

} else {
	$("#speech-btn").addClass("display-none");
}

/*
 * SPEECH RECOGNITION ENGINE
 * Copyright (c) 2013 Tal Ater
 * Modified by MyOrange
 * All modifications made are hereby copyright (c) 2014 MyOrange
 */

(function(undefined) {"use strict";

	// Check browser support
	// This is done as early as possible, to make it as fast as possible for unsupported browsers
	if (!SpeechRecognition) {
		root.smartSpeechRecognition = null;
		return undefined;
	}

	var commandsList = [], recognition, callbacks = {
		start : [],
		error : [],
		end : [],
		result : [],
		resultMatch : [],
		resultNoMatch : [],
		errorNetwork : [],
		errorPermissionBlocked : [],
		errorPermissionDenied : []
	}, autoRestart, lastStartedAt = 0,
	//debugState = false, // decleared in app.config.js
	//debugStyle = 'font-weight: bold; color: #00f;', // decleared in app.config.js

	// The command matching code is a modified version of Backbone.Router by Jeremy Ashkenas, under the MIT license.
	optionalParam = /\s*\((.*?)\)\s*/g, optionalRegex = /(\(\?:[^)]+\))\?/g, namedParam = /(\(\?)?:\w+/g, splatParam = /\*\w+/g, escapeRegExp = /[\-{}\[\]+?.,\\\^$|#]/g, commandToRegExp = function(command) {
		command = command.replace(escapeRegExp, '\\$&').replace(optionalParam, '(?:$1)?').replace(namedParam, function(match, optional) {
			return optional ? match : '([^\\s]+)';
		}).replace(splatParam, '(.*?)').replace(optionalRegex, '\\s*$1?\\s*');
		return new RegExp('^' + command + '$', 'i');
	};

	// This method receives an array of callbacks to iterate over, and invokes each of them
	var invokeCallbacks = function(callbacks) {
		callbacks.forEach(function(callback) {
			callback.callback.apply(callback.context);
		});
	};

	var initIfNeeded = function() {
		if (!isInitialized()) {
			root.smartSpeechRecognition.init({}, false);
		}
	};

	var isInitialized = function() {
		return recognition !== undefined;
	};

	root.smartSpeechRecognition = {
		// Initialize smartSpeechRecognition with a list of commands to recognize.
		// e.g. smartSpeechRecognition.init({'hello :name': helloFunction})
		// smartSpeechRecognition understands commands with named variables, splats, and optional words.
		init : function(commands, resetCommands) {

			// resetCommands defaults to true
			if (resetCommands === undefined) {
				resetCommands = true;
			} else {
				resetCommands = !!resetCommands;
			}

			// Abort previous instances of recognition already running
			if (recognition && recognition.abort) {
				recognition.abort();
			}

			// initiate SpeechRecognition
			recognition = new SpeechRecognition();

			// Set the max number of alternative transcripts to try and match with a command
			recognition.maxAlternatives = 5;
			recognition.continuous = true;
			// Sets the language to the default 'en-US'. This can be changed with smartSpeechRecognition.setLanguage()
			recognition.lang = voice_command_lang || 'en-US';

			recognition.onstart = function() {
				invokeCallbacks(callbacks.start);
				//debugState
				if (debugState) {
					root.console.log('%c ✔ SUCCESS: User allowed access the microphone service to start ', debugStyle_success);
					root.console.log('Language setting is set to: ' + recognition.lang, debugStyle);
				}
				$.root_.removeClass("service-not-allowed");
				$.root_.addClass("service-allowed");
			};

			recognition.onerror = function(event) {
				invokeCallbacks(callbacks.error);
				switch (event.error) {
					case 'network':
						invokeCallbacks(callbacks.errorNetwork);
						break;
					case 'not-allowed':
					case 'service-not-allowed':
						// if permission to use the mic is denied, turn off auto-restart
						autoRestart = false;
						$.root_.removeClass("service-allowed");
						$.root_.addClass("service-not-allowed");
						//debugState
						if (debugState) {
							root.console.log('%c WARNING: Microphone was not detected (either user denied access or it is not installed properly) ', debugStyle_warning);
						}
						// determine if permission was denied by user or automatically.
						if (new Date().getTime() - lastStartedAt < 200) {
							invokeCallbacks(callbacks.errorPermissionBlocked);
						} else {
							invokeCallbacks(callbacks.errorPermissionDenied);
							//console.log("You need your mic to be active")
						}
						break;
				}
			};

			recognition.onend = function() {
				invokeCallbacks(callbacks.end);
				// smartSpeechRecognition will auto restart if it is closed automatically and not by user action.
				if (autoRestart) {
					// play nicely with the browser, and never restart smartSpeechRecognition automatically more than once per second
					var timeSinceLastStart = new Date().getTime() - lastStartedAt;
					if (timeSinceLastStart < 1000) {
						setTimeout(root.smartSpeechRecognition.start, 1000 - timeSinceLastStart);
					} else {
						root.smartSpeechRecognition.start();
					}
				}
			};

			recognition.onresult = function(event) {
				invokeCallbacks(callbacks.result);

				var results = event.results[event.resultIndex], commandText;

				// go over each of the 5 results and alternative results received (we've set maxAlternatives to 5 above)
				for (var i = 0; i < results.length; i++) {
					// the text recognized
					commandText = results[i].transcript.trim();
					if (debugState) {
						root.console.log('Speech recognized: %c' + commandText, debugStyle);
					}

					// try and match recognized text to one of the commands on the list
					for (var j = 0, l = commandsList.length; j < l; j++) {
						var result = commandsList[j].command.exec(commandText);
						if (result) {
							var parameters = result.slice(1);
							if (debugState) {
								root.console.log('command matched: %c' + commandsList[j].originalPhrase, debugStyle);
								if (parameters.length) {
									root.console.log('with parameters', parameters);
								}
							}
							// execute the matched command
							commandsList[j].callback.apply(this, parameters);
							invokeCallbacks(callbacks.resultMatch);

							// for commands "sound on", "stop" and "mute" do not play sound or display message
							//var myMatchedCommand = commandsList[j].originalPhrase;

							var ignoreCallsFor = ["sound on", "mute", "stop"];

							if (ignoreCallsFor.indexOf(commandsList[j].originalPhrase) < 0) {
								// play sound when match found
								$.smallBox({
									title : (commandsList[j].originalPhrase),
									content : "loading...",
									color : "#333",
									sound_file : 'voice_alert',
									timeout : 2000
								});

								if ($('#speech-btn .popover').is(':visible')) {
									$('#speech-btn .popover').fadeOut(250);
								}
							}// end if

							return true;
						}
					} // end for
				}// end for

				invokeCallbacks(callbacks.resultNoMatch);
				//console.log("no match found for: " + commandText)
				$.smallBox({
					title : "Error: <strong>" + ' " ' + commandText + ' " ' + "</strong> no match found!",
					content : "Please speak clearly into the microphone",
					color : "#a90329",
					timeout : 5000,
					icon : "fa fa-microphone"
				});
				if ($('#speech-btn .popover').is(':visible')) {
					$('#speech-btn .popover').fadeOut(250);
				}
				return false;
			};

			// build commands list
			if (resetCommands) {
				commandsList = [];
			}
			if (commands.length) {
				this.addCommands(commands);
			}
		},

		// Start listening (asking for permission first, if needed).
		// Call this after you've initialized smartSpeechRecognition with commands.
		// Receives an optional options object:
		// { autoRestart: true }
		start : function(options) {
			initIfNeeded();
			options = options || {};
			if (options.autoRestart !== undefined) {
				autoRestart = !!options.autoRestart;
			} else {
				autoRestart = true;
			}
			lastStartedAt = new Date().getTime();
			recognition.start();
		},

		// abort the listening session (aka stop)
		abort : function() {
			autoRestart = false;
			if (isInitialized) {
				recognition.abort();
			}
		},

		// Turn on output of debug messages to the console. Ugly, but super-handy!
		debug : function(newState) {
			if (arguments.length > 0) {
				debugState = !!newState;
			} else {
				debugState = true;
			}
		},

		// Set the language the user will speak in. If not called, defaults to 'en-US'.
		// e.g. 'fr-FR' (French-France), 'es-CR' (Español-Costa Rica)
		setLanguage : function(language) {
			initIfNeeded();
			recognition.lang = language;
		},

		// Add additional commands that smartSpeechRecognition will respond to. Similar in syntax to smartSpeechRecognition.init()
		addCommands : function(commands) {
			var cb, command;

			initIfNeeded();

			for (var phrase in commands) {
				if (commands.hasOwnProperty(phrase)) {
					cb = root[commands[phrase]] || commands[phrase];
					if ( typeof cb !== 'function') {
						continue;
					}
					//convert command to regex
					command = commandToRegExp(phrase);

					commandsList.push({
						command : command,
						callback : cb,
						originalPhrase : phrase
					});
				}
			}
			if (debugState) {
				root.console.log('Commands successfully loaded: %c' + commandsList.length, debugStyle);
			}
		},

		// Remove existing commands. Called with a single phrase, array of phrases, or methodically. Pass no params to remove all commands.
		removeCommands : function(commandsToRemove) {
			if (commandsToRemove === undefined) {
				commandsList = [];
				return;
			}
			commandsToRemove = Array.isArray(commandsToRemove) ? commandsToRemove : [commandsToRemove];
			commandsList = commandsList.filter(function(command) {
				for (var i = 0; i < commandsToRemove.length; i++) {
					if (commandsToRemove[i] === command.originalPhrase) {
						return false;
					}
				}
				return true;
			});
		},

		// Lets the user add a callback of one of 9 types:
		// start, error, end, result, resultMatch, resultNoMatch, errorNetwork, errorPermissionBlocked, errorPermissionDenied
		// Can also optionally receive a context for the callback function as the third argument
		addCallback : function(type, callback, context) {
			if (callbacks[type] === undefined) {
				return;
			}
			var cb = root[callback] || callback;
			if ( typeof cb !== 'function') {
				return;
			}
			callbacks[type].push({
				callback : cb,
				context : context || this
			});
		}
	};

}).call(this);

var autoStart = function() {

	smartSpeechRecognition.addCommands(commands);

	if (smartSpeechRecognition) {
		// activate plugin
		smartSpeechRecognition.start();
		// add btn class
		$.root_.addClass("voice-command-active");
		// set localStorage when switch is on manually
		if (voice_localStorage) {
			localStorage.setItem('sm-setautovoice', 'true');
		}

	} else {
		// if plugin not found
		alert("speech plugin not loaded");
	}
}
// if already running with localstorage
if (SpeechRecognition && voice_command && localStorage.getItem('sm-setautovoice') == 'true') {
	autoStart();
}

// auto start
if (SpeechRecognition && voice_command_auto && voice_command) {
	autoStart();
}