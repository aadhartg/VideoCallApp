  
      var slideout = new Slideout({
        'panel': document.getElementById('panel'),
        'menu': document.getElementById('mbl-menu'),
        'padding': 0,
        'tolerance': 0
      });

      // Toggle button
      document.querySelector('.toggle-button').addEventListener('click', function() {
        slideout.toggle();
      });

	
   
        $(window).ready(function(){
            $(".ag-top-nav .dropdown").hover(function(){
                $(this).addClass("open");
            },function() {
                $(this).removeClass('open');
            });
            $(".ag-btm-nav .dropdown").hover(function(){
                $(this).addClass("open");
                $('html').addClass('overlay');
            },function() {
                $(this).removeClass('open');
                $('html').removeClass('overlay');
            });
        }); 
        
   
        $(window).bind("resize", function () {
            console.log($(this).width())
            if ($(this).width() < 769) {
                $('.hdr--search-container').addClass('collapse')
                $('#agFilterCollapse').addClass('collapse')
            } else {
                $('.hdr--search-container').removeClass('collapse')
                $('#agFilterCollapse').removeClass('collapse')
            }
        }).trigger('resize');
  
        $(window).scroll(function(){
            if ($(this).scrollTop() > 50) {
               $('.ag-body-bg').addClass('fixedfilter');
            } else {
               $('.ag-body-bg').removeClass('fixedfilter');
            }
        });
 

		var swiper = new Swiper('.homebanner-slider', {
		  slidesPerView: 1,
		  loop: true,
		  pagination: {
			el: '.swiper-pagination',
			clickable: true,
            dynamicBullets: true,
		  },
		  navigation: {
			nextEl: '.swiper-button-next',
			prevEl: '.swiper-button-prev',
		  },
		});    
          
          
        var swiper = new Swiper('.topdeals-slider', {
          slidesPerView: 2,
          spaceBetween: 10,
          navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
          },
          breakpoints: {
            640: {
              slidesPerView: 4,
              spaceBetween: 2,
            },
            768: {
              slidesPerView: 4,
              spaceBetween: 5,
            },
            1024: {
              slidesPerView: 5,
            },
            1200: {
              slidesPerView: 6,
            },              
            1400: {
              slidesPerView: 7,
            },
          }
        });
  
          
        var swiper = new Swiper('.category-brands-slider', {
		  slidesPerView: 3,
		  loop: false,
          freeMode: false,
		  pagination: false,
          navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
          },
          breakpoints: {
            414: {
              slidesPerView: 5,
            },
            991: {
              slidesPerView: 6,
            },              
            1200: {
              slidesPerView: 7,
            },              
            1400: {
              slidesPerView: 8,
            },
          }
		});
		