/*if ($('.tree > ul') && !mytreebranch) {

	var mytreebranch = $('.tree').find('li:has(ul)').addClass('parent_li').attr('role', 'treeitem').find(' > span').attr('title', 'Collapse this branch');

	$('.tree > ul').attr('role', 'tree').find('ul').attr('role', 'group');
	mytreebranch.on('click', function (e) {
		var children = $(this).parent('li.parent_li').find(' > ul > li');
		if (children.is(':visible')) {
			children.hide('fast');
			$(this).attr('title', 'Expand this branch').find(' > i').addClass('icon-plus-sign').removeClass('icon-minus-sign');
		}
		else {
			children.show('fast');
			$(this).attr('title', 'Collapse this branch').find(' > i').addClass('icon-minus-sign').removeClass('icon-plus-sign');
		}
		e.stopPropagation();
	});

}*/


/**
 * A jQuery plugin boilerplate.
 * Author: Jonathan Nicol @f6design
 */
;(function($) {  
  // Change this to your plugin name. 
  var pluginName = 'treeView';
  
  /**
   * Plugin object constructor.
   * Implements the Revealing Module Pattern.
   */
  function Plugin(element, options) {
	// References to DOM and jQuery versions of element.
	var el = element;
	var $el = $(element);
	options = $.extend({}, $.fn[pluginName].defaults, options);

	/**
	 * Initialize plugin.
	 */
	function init() {
	  // Add any initialization logic here...

var mytreebranch = $('.tree').find('li:has(ul)').addClass('parent_li').attr('role', 'treeitem').find(' > span').attr('title', 'Collapse this branch');

	$('.tree > ul').attr('role', 'tree').find('ul').attr('role', 'group');
	mytreebranch.on('click', function (e) {
		var children = $(this).parent('li.parent_li').find(' > ul > li');
		if (children.is(':visible')) {
			children.hide('fast');
			$(this).attr('title', 'Expand this branch').find(' > i').addClass('icon-plus-sign').removeClass('icon-minus-sign');
		}
		else {
			children.show('fast');
			$(this).attr('title', 'Collapse this branch').find(' > i').addClass('icon-minus-sign').removeClass('icon-plus-sign');
		}
		e.stopPropagation();
	});

	  hook('onInit');
	}

	/**
	 * Example Public Method
	 */
	function fooPublic() {
	  // Code goes here...
	}

	/**
	 * Get/set a plugin option.
	 * Get usage: $('#el').demoplugin('option', 'key');
	 * Set usage: $('#el').demoplugin('option', 'key', value);
	 */
	function option (key, val) {
	  if (val) {
		options[key] = val;
	  } else {
		return options[key];
	  }
	}

	/**
	 * Destroy plugin.
	 * Usage: $('#el').demoplugin('destroy');
	 */
	function destroy() {
	  // Iterate over each matching element.
	  $el.each(function() {
		var el = this;
		var $el = $(this);

		// Add code to restore the element to its original state...

		hook('onDestroy');
		// Remove Plugin instance from the element.
		$el.removeData('plugin_' + pluginName);
	  });
	}

	/**
	 * Callback hooks.
	 * Usage: In the defaults object specify a callback function:
	 * hookName: function() {}
	 * Then somewhere in the plugin trigger the callback:
	 * hook('hookName');
	 */
	function hook(hookName) {
	  if (options[hookName] !== undefined) {
		// Call the user defined function.
		// Scope is set to the jQuery element we are operating on.
		options[hookName].call(el);
	  }
	}

	// Initialize the plugin instance.
	init();

	// Expose methods of Plugin we wish to be public.
	return {
	  option: option,
	  destroy: destroy,
	  fooPublic: fooPublic
	};
  }

  /**
   * Plugin definition.
   */
  $.fn[pluginName] = function(options) {
	// If the first parameter is a string, treat this as a call to
	// a public method.
	if (typeof arguments[0] === 'string') {
	  var methodName = arguments[0];
	  var args = Array.prototype.slice.call(arguments, 1);
	  var returnVal;
	  this.each(function() {
		// Check that the element has a plugin instance, and that
		// the requested public method exists.
		if ($.data(this, 'plugin_' + pluginName) && typeof $.data(this, 'plugin_' + pluginName)[methodName] === 'function') {
		  // Call the method of the Plugin instance, and Pass it
		  // the supplied arguments.
		  returnVal = $.data(this, 'plugin_' + pluginName)[methodName].apply(this, args);
		} else {
		  throw new Error('Method ' +  methodName + ' does not exist on jQuery.' + pluginName);
		}
	  });
	  if (returnVal !== undefined){
		// If the method returned a value, return the value.
		return returnVal;
	  } else {
		// Otherwise, returning 'this' preserves chainability.
		return this;
	  }
	// If the first parameter is an object (options), or was omitted,
	// instantiate a new instance of the plugin.
	} else if (typeof options === "object" || !options) {
	  return this.each(function() {
		// Only allow the plugin to be instantiated once.
		if (!$.data(this, 'plugin_' + pluginName)) {
		  $.data(this, 'plugin_' + pluginName, new Plugin(this, options));
		}
	  });
	}
  };

  // Default plugin options.
  $.fn[pluginName].defaults = {
	onInit: function() {},
	onDestroy: function() {},
	element: $('.tree > ul')
  };

})(jQuery);


$('.tree').treeView({
	element: $('.tree > ul')
});