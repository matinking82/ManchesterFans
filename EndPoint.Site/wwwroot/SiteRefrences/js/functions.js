/*------------------------------------
	Theme Name: Reporter
	Start Date : 17-Jan-2017
	End Date : 
	Last change:  31-Jan-2017
	Version: 1.0
	Assigned to:
	Primary use:
---------------------------------------*/
/*	

	+ Responsive Caret
	+ Expand Panel Resize
	+ Largest Post
	+ Google Map
	+ Sticky Menu
	
	+ Document On Ready
		- Scrolling Navigation
		- Set Sticky Menu
		- Responsive Caret
		- Expand Panel
		- Collapse Panel
		- Menu Switch
		- Largest Post
		- Video Carousel
		- Skill Block
		- Contact Map
		- Quick Contact Form
	
	+ Window On Scroll
		- Set Sticky Menu
		
	 + Window On Resize
		- Expand Panel Resize
		- Menu Switch
		
	+ Window On Load
		- Site Loader
		- Largest Post
		
*/

(function($) {

	"use strict" 	
	
	/* + Responsive Caret* */
	function menu_dropdown_open(){
		var width = $(window).width();
		if($(".ownavigation .nav li.ddl-active").length ) {
			if( width > 991 ) {
				$(".ownavigation .nav > li").removeClass("ddl-active");
				$(".ownavigation .nav li .dropdown-menu").removeAttr("style");
			}
		} else {
			$(".ownavigation .nav li .dropdown-menu").removeAttr("style");
		}
	}
	
	/* + Expand Panel Resize * */
	function panel_resize(){
		var width = $(window).width();
		if( width > 991 ) {
			if($(".header_s #slidepanel").length ) {
				$(".header_s #slidepanel").removeAttr("style");
			}
		}
	}
	
	/* + Largest Post */
	function largepost() {
		if($(".larg-post-full-container").length){
			var $container = $(".larg-post-full-container");
			$container.isotope({
				layoutMode: 'masonry',
				itemSelector: ".post-box",
				gutter: 0,
				transitionDuration: "0.5s"
			});
		}
	}
	
	/* + Google Map* */
	function initialize(obj) {
		var lat = $("#"+obj).attr("data-lat");
        var lng = $("#"+obj).attr("data-lng");
		var contentString = $("#"+obj).attr("data-string");
		var myLatlng = new google.maps.LatLng(lat,lng);
		var map, marker, infowindow;
		var image = "assets/images/marker.png";
		var zoomLevel = parseInt($("#"+obj).attr("data-zoom") ,10);		
		var styles = [{"featureType": "administrative.country","elementType": "geometry","stylers": [{"visibility": "simplified"},{"hue":"#ff0000"}]}]
		var styledMap = new google.maps.StyledMapType(styles,{name: "Styled Map"});	
		
		var mapOptions = {
			zoom: zoomLevel,
			disableDefaultUI: true,
			center: myLatlng,
            scrollwheel: false,
			mapTypeControlOptions: {
            mapTypeIds: [google.maps.MapTypeId.ROADMAP, "map_style"]
			}
		}
		
		map = new google.maps.Map(document.getElementById(obj), mapOptions);	
		
		map.mapTypes.set("map_style", styledMap);
		map.setMapTypeId("map_style");
		
		infowindow = new google.maps.InfoWindow({
			content: contentString
		});      
	    
        marker = new google.maps.Marker({
			position: myLatlng,
			map: map,
			icon: image
		});

		google.maps.event.addListener(marker, "click", function() {
			infowindow.open(map,marker);
		});
	}
	
	/* + Sticky Menu */
	function sticky_menu() {
		var menu_scroll = $("body").offset().top;
		var scroll_top = $(window).scrollTop();
		
		if ( scroll_top > menu_scroll ) {
			$(".ownavigation").addClass("navbar-fixed-top animated fadeInDown");
		} else {
			$(".ownavigation").removeClass("navbar-fixed-top animated fadeInDown"); 
		}
	}
	
	/* + Document On Ready */
	$(document).on("ready", function() {

		/* - Scrolling Navigation* */
		var width	=	$(window).width();
		var height	=	$(window).height();
		
		/* - Set Sticky Menu* */
		if( $(".header_s").length ) {
			sticky_menu();
		}
		
		$('.navbar-nav li a[href*="#"]:not([href="#"]), .site-logo a[href*="#"]:not([href="#"])').on("click", function(e) {
	
			var $anchor = $(this);
			
			$("html, body").stop().animate({ scrollTop: $($anchor.attr("href")).offset().top - 49 }, 1500, "easeInOutExpo");
			
			e.preventDefault();
		});

		/* - Responsive Caret* */
		$(".ddl-switch").on("click", function() {
			var li = $(this).parent();
			if ( li.hasClass("ddl-active") || li.find(".ddl-active").length !== 0 || li.find(".dropdown-menu").is(":visible") ) {
				li.removeClass("ddl-active");
				li.children().find(".ddl-active").removeClass("ddl-active");
				li.children(".dropdown-menu").slideUp();
			}
			else {
				li.addClass("ddl-active");
				li.children(".dropdown-menu").slideDown();
			}
		});
		
		/* - Expand Panel * */
		$("#slideit").on ("click", function() {
			$("#slidepanel").slideDown(1000);
			$("html").animate({ scrollTop: 0 }, 1000);
		});	

		/* - Collapse Panel * */
		$("#closeit").on("click", function() {
			$("#slidepanel").slideUp("slow");
			$("html").animate({ scrollTop: 0 }, 1000);
		});	
		
		/* Switch buttons from "Log In | Register" to "Close Panel" on click * */
		$("#toggle a").on("click", function() {
			$("#toggle a").toggle();
		});	
		
		panel_resize();
		
		/* - Top Menu */
		$(".top-header .container").prepend("<span></span>");
		if($(".top-menu").length){
			$(".top-menu > a").on("click", function(){
				$(".top-social > ul").removeClass("active");
				$(".top-menu > ul").toggleClass("active");
				if($("html[dir='rtl']").length) {
					if($(".top-menu > ul.active").length) {
						$(".top-header .container > span").css("right","50%");
					} else {
						$(".top-header .container > span").css("right","15px");
					}
					if($(".top-social > ul.active").length) {
						$(".top-header .container > span").css("left","50%");
					} else {
						$(".top-header .container > span").css("left","15px");
					}
				} else {
					if($(".top-menu > ul.active").length) {
						$(".top-header .container > span").css("left","50%");
					} else {
						$(".top-header .container > span").css("left","15px");
					}
					if($(".top-social > ul.active").length) {
						$(".top-header .container > span").css("right","50%");
					} else {
						$(".top-header .container > span").css("right","15px");
					}

				}
			});			
		}
		
		/* - Top Social */
		if($(".top-social").length){
			$(".top-social > a").on("click", function(){
				$(".top-menu > ul").removeClass("active");
				$(".top-social > ul").toggleClass("active");
				if($("html[dir='rtl']").length) {
					if($(".top-menu > ul.active").length) {
						$(".top-header .container > span").css("right","50%");
					} else {
						$(".top-header .container > span").css("right","15px");
					}
					if($(".top-social > ul.active").length) {
						$(".top-header .container > span").css("left","50%");
					} else {
						$(".top-header .container > span").css("left","15px");
					}
				} else {
					if($(".top-menu > ul.active").length) {
						$(".top-header .container > span").css("left","50%");
					} else {
						$(".top-header .container > span").css("left","15px");
					}
					if($(".top-social > ul.active").length) {
						$(".top-header .container > span").css("right","50%");
					} else {
						$(".top-header .container > span").css("right","15px");
					}
				}
			});
		}
		
		$( ".top-search .searchform .form-control" ).on("focus", function() {
			if($("html[dir='rtl']").length) {
				$(".top-header .container > span").css("left","50%");
			} else {
				$(".top-header .container > span").css("right","50%");
			}
		});
		$( ".top-search .searchform .form-control" ).on("focusout", function() {
			if($("html[dir='rtl']").length) {
				$(".top-header .container > span").css("left","15px");
			} else {
				$(".top-header .container > span").css("right","15px");
			}
		});
		
		/* - Largest Post */
		largepost();
		
		/* - Video Carousel */
		if( $(".latest-video-block").length ) {
			if($('html[dir="rtl"]').length) {
				$("#latest_video_block").owlCarousel({
					loop: true,
					margin: 0,
					nav: false,
					dots: false,
					rtl: true,
					autoplay: false,
					responsive:{
						0:{
							items: 1
						},
						640:{
							items: 2
						},
						992:{
							items: 3
						},
					}
				});
			} else {
				$("#latest_video_block").owlCarousel({
					loop: true,
					margin: 0,
					nav: false,
					dots: false,
					autoplay: false,
					responsive:{
						0:{
							items: 1
						},
						640:{
							items: 2
						},
						992:{
							items: 3
						},
					}
				});
			}
			$(".latest-video-block .lr-nav .nav-right").on("click",function(){
				$("#latest_video_block").trigger('next.owl.carousel');
			})
			$(".latest-video-block .lr-nav .nav-left").on("click",function(){
				$("#latest_video_block").trigger('prev.owl.carousel');
			})
		}
		
		if( $(".full-latest-video-block").length ) {
			if($('html[dir="rtl"]').length) {
				$("#full_latest_video_block").owlCarousel({
					loop: true,
					margin: 0,
					nav: false,
					dots: false,
					rtl: true,
					autoplay: false,
					responsive:{
						0:{
							items: 1
						},
						640:{
							items: 2
						},
						992:{
							items: 3
						},
						1366:{
							items: 4
						},
					}
				});
			} else {
				$("#full_latest_video_block").owlCarousel({
					loop: true,
					margin: 0,
					nav: false,
					dots: false,
					autoplay: false,
					responsive:{
						0:{
							items: 1
						},
						640:{
							items: 2
						},
						992:{
							items: 3
						},
						1366:{
							items: 4
						},
					}
				});
			}
			$(".full-latest-video-block .lr-nav .nav-right").on("click",function(){
				$("#full_latest_video_block").trigger('next.owl.carousel');
			})
			$(".full-latest-video-block .lr-nav .nav-left").on("click",function(){
				$("#full_latest_video_block").trigger('prev.owl.carousel');
			})
		}
		
		/* - Skill Block */
		$( "[id*='skill_type-']" ).each(function ()
		{
			var ele_id = 0;
			ele_id = $(this).attr('id').split("-")[1];
			
			var $this = $(this);
			var myVal = $(this).data("value");

			$this.appear(function()
			{			
				var skill_type1_item_count = 0;
				var skill_type1_count = 0;					
				skill_type1_item_count = $( "[id*='skill_"+ ele_id +"_count-']" ).length;				
				
				for(var i=1; i<=skill_type1_item_count; i++) 
				{
					skill_type1_count = $( "[id*='skill_"+ ele_id +"_count-"+i+"']" ).attr( "data-skill_percent" );
					$("[id*='skill_"+ ele_id +"_count-"+i+"']").animateNumber({ number: skill_type1_count }, 5000);
					if($('html[dir="rtl"]').length) {
						$("[id*='skill_"+ ele_id +"_count-"+i+"']").css("right",skill_type1_count +"%");
					} else {
						$("[id*='skill_"+ ele_id +"_count-"+i+"']").css("left",skill_type1_count +"%");
					}
				}
				
				var skill_bar_count = 0;
				var skills_bar_count = 0;
				skill_bar_count = $( "[id*='skill_bar_"+ ele_id +"_count-']" ).length;
				
				for(var j=1; j<=skill_bar_count; j++) 
				{
					skills_bar_count = $( "[id*='skill_"+ ele_id +"_count-"+j+"']" ).attr( "data-skill_percent" );
					$("[id*='skill_bar_"+ ele_id +"_count-"+j+"']").css({'width': skills_bar_count +'%'});
				}
			});
		});
		
		/* - Contact Map* */
		if($("#map-canvas-contact").length==1){
			initialize("map-canvas-contact");
		}
		
		/* - Quick Contact Form* */
		$( "#btn_submit" ).on( "click", function(event) {
		  event.preventDefault();
		  var mydata = $("form").serialize();
			$.ajax({
				type: "POST",
				dataType: "json",
				url: "contact.php",
				data: mydata,
				success: function(data) {
					if( data["type"] == "error" ){
						$("#alert-msg").html(data["msg"]);
						$("#alert-msg").removeClass("alert-msg-success");
						$("#alert-msg").addClass("alert-msg-failure");
						$("#alert-msg").show();
					} else {
						$("#alert-msg").html(data["msg"]);
						$("#alert-msg").addClass("alert-msg-success");
						$("#alert-msg").removeClass("alert-msg-failure");					
						$("#input_name").val("");
						$("#input_phone").val("");												
						$("#input_email").val("");												
						$("#input_address").val("");												
						$("#textarea_message").val("");
						$("#alert-msg").show();				
					}			
				},
				error: function(xhr, textStatus, errorThrown) {
					alert(textStatus);
				}
			});
		});
		
	});	/* - Document On Ready /- */
	
	/* + Window On Scroll */
	$(window).on("scroll",function() {
		/* - Set Sticky Menu* */
		if( $(".header_s").length ) {
			sticky_menu();
		}
	});
	
	/* + Window On Resize */ 
	$( window ).on("resize",function() {
		var width	=	$(window).width();
		var height	=	$(window).height();
		
		/* - Expand Panel Resize */
		panel_resize();
		menu_dropdown_open();
	});
	
	/* + Window On Load */
	$(window).on("load",function() {
		/* - Site Loader* */
		if ( !$("html").is(".ie6, .ie7, .ie8") ) {
			$("#site-loader").delay(1000).fadeOut("slow");
		}
		else {
			$("#site-loader").css("display","none");
		}
		
		/* - Largest Post */
		largepost();
	});

})(jQuery);