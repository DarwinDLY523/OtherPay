// =================================================================== 
// 项目说明
//====================================================================
// 作者：OtherPay
// 创建时间：2016/3/12
// ===================================================================
Namespace = new Object();
Namespace.register = function (fullNS) {
    var nsArray = fullNS.split('.');
    var sEval = "";
    var sNS = "";
    var count = nsArray.length;
    for (var i = 0; i < count; i++) {
        if (i != 0) sNS += ".";
        sNS += nsArray[i];
        if (i < count - 1) {
            sEval += "if (typeof(" + sNS + ") == 'undefined') " + sNS + " = new Object();";
        } else {
            sEval += "delete " + sNS + ";" + sNS + " = new Object();";
        };
    }
    if (sEval != "") eval(sEval);
};
Namespace.register("OtherPay.FN");
Namespace.register("OtherPay.ACT");
Namespace.register("OtherPay.MSG");
Namespace.register("OtherPay.Services");
OtherPay.Services = {
    Url: "",
    Type: "" //判断是企业号/服务号
}
var wait = 60;//60秒倒计时
var oFF = true;
var isSubmit = true;//是否提交
var loading = true;//是否显蒙层
OtherPay.MSG = {
    //提示信息
    alert: function (message, timeout, callback) {
        var pWidth = "60%";
        var pHeight = "10%";

        var currentDot = $("<div />", {
            id: "comMask"
        })
        var promptBox = $("<div />", {
            class: 'prompt-box',
            css: {
                width: pWidth,
                height: pHeight
            },
            text: message
        });

        currentDot.appendTo("body");
        promptBox.appendTo("body");

        $(document).ready(function () {
            PH = 0 - $(".prompt-box").height() / 2
            PW = 0 - $(".prompt-box").width() / 2
            $(".prompt-box").css({ marginLeft: PW, marginTop: PH })
        });

        $(window).resize(function () {
            PH = 0 - $(".prompt-box").height() / 2
            PW = 0 - $(".prompt-box").width() / 2
            $(".prompt-box").css({ marginLeft: PW, marginTop: PH })
        });

        $(".prompt-box").click(function () {
            return false;
        })

        $("#comMask").click(function () {
            $(".prompt-box").remove();
            $("#comMask").remove();
            if (typeof callback === "function") {
                callback();
            }
        });
        if (timeout != undefined && timeout > 0) {
            setTimeout(function () {
                if (typeof callback === "function") {
                    callback();
                }
                else {
                    $(".prompt-box").remove();
                    $("#comMask").remove();
                }
            }, timeout);
        }

    },
    //加载中
    loadingBegin: function (callback) {
        var loadingBox = $("<div  id='loading-box'><span>加载中<i>.</i><i>..</i></i><i>...</i></span></div>");
        loadingBox.appendTo("body");
        var now = 0;
        window.CommonLoadingTimer = setInterval(function () {
            now++;
            if (now === 3) {
                now = 0
            }
            $('#loading-box').find('i').eq(now).show().siblings('i').hide();
        }, 500)
        if (callback) {
            callback();
        }
    },
    //加载结束
    loadingEnd: function (callback) {
        $("#loading-box").remove();
        clearInterval(window.CommonLoadingTimer);
        if (callback) {
            callback();
        }
    },
    //蒙版显示
    maskDisplay: function (callback) {
        var maskBox = $("<div/>", {
            id: "mask-borad"
        });
        maskBox.appendTo("body");
        if (typeof callback === "function") {
            callback();
        }
        if (callback != true) {
            $(window).bind("touchmove", function (e) {
                e.preventDefault();
            });
        }
    },
    //蒙版隐藏
    maskHide: function (callback) {
        if (window.sharedisplay == "true") {
            setTimeout(function () {
                $("#mask-borad").remove();
            }, 400)
        } else {
            $("#mask-borad").remove();
        }

        $(window).unbind("touchmove");
    },
    //阻止浏览器事件
    _stopEvenFnt: function (e) {
        e.preventDefault()
    },
    addStopEvent: function () {
        document.addEventListener('touchmove', this._stopEventFn)
    },
    removeStopEvent: function () {
        document.removeEventListener('touchmove', this._stopEventFn)
    },
    //tab切换
    tabSlider: function (opts, cb) {
        $(opts).each(function (i) {
            (function (i) {
                $(opts[i].hd).on('click', function () {
                    $(opts).each(function (j) {
                        $(opts[j].hd).removeClass('active');
                        $(opts[j].bd).hide();
                    })
                    $(opts[i].hd).addClass('active')
                    $(opts[i].bd).show();
                    if (cb) {
                        cb();
                    }
                })
            })(i)
        })
    },
    /**
     * 弹窗按钮
     */
    fixedSearchBtn: function (search_callback, chose_callback) {
        var self = this;
        self._rightsideBtnOuterInit({
            order: 20,
            html: '<a href="javascript:" id="fixedSearchBtn" class="__rightside_btn_search"></a>'
        });

        // var _fixedSearchBtn = '<a href="javascript:" id="fixedSearchBtn" class="fixedSearchBtn"></a>';
        // $('body').append(_fixedSearchBtn);


        $(document).on('click', '#fixedSearchBtn', function () {
            self._openSeachModel(search_callback, chose_callback);
            // setTimeout(function(){
            $('#search_model .search .tex').focus();
            // },100)
        });


    },
    /**
     * 打开搜索弹窗
     * @param  {function} search_callback 按下回车回调
     * @param  {function} chose_callback 点击搜索完的列表的回调 通过参数可以获取点击的那个
     */
    _openSeachModel: function (search_callback, chose_callback) {
        var _html = '<div class="search_model active" id="search_model">' +
            '    <div class="pd20 pl30 pr30 mb30 halfborderbottom search_content">' +
            '        <!-- 必须是form标签 -->' +
            '        <form onsubmit="return false" class="search halfborder">' +
            '            <input type="search" class="tex" id="search_model_texinput" placeholder="输入楼盘名称、客户姓名、合同编号搜索">' +
            '        </form>' +
            '       <a href="javascript:" class="cancel_search info_blue">取消</a>' +
            '    </div>' +
            '    <div class="builds_list">' +
            '    </div>' +
            '</div>'

        $('body').append(_html);
        // $('#search_model').addClass('active');
        $('body').css({
            position: 'absolute',
            height: '100%',
            overflow: 'hidden'
        });

        $('#search_model .cancel_search').on('click', function () {
            OtherPay.MSG.closeSearchModel();
        });
        $(document).on('click', '#search_model .builds_item', function () {
            OtherPay.MSG.closeSearchModel();
            var self = $(this);
            if (chose_callback) {
                chose_callback(self);
            }
        });

        $(document).on('keyup', function (e) {
            // enter 键
            if (e && e.keyCode == 13) {
                var val = $('#search_model_texinput').val();
                if (search_callback) {
                    search_callback(val);
                }
                $('#search_model_texinput').blur()
            }
        })

    },
    //关闭搜索弹窗
    closeSearchModel: function () {
        $('#search_model').remove();
        $('#search_model .cancel_search,#search_model .builds_item').off('click')
        $(document).off('keyup')
        $('body').css({
            position: 'static',
            height: 'auto',
            overflowX: 'hidden',
            overflowY: 'visible'
        });
    },
    //弹窗询问公共
    //title 标题文字
    //comfimbtncallback 点击确定回调
    //cancelbtncallback 点击取消回调
    //textarea: 布尔值 是否要输入框 或者直接指定textarea_id
    //textarea_id:输入框id
    //textarea_placeholder: 输入框placeholder
    confirmModel: function (opts) {
        var _id = opts.id
        var _title = opts.title
        var _maskid = _id + '_mask';
        var _textareahtml = '';
        var _textarea_id = _id + '_textarea';
        var _textarea_placeholder = '请输入' + _title;

        if (opts.textarea_id) {
            _textarea_id = opts.textarea_id;
        }
        if (opts.textarea_placeholder) {
            _textarea_id = opts.textarea_id;
        }
        if (opts.textarea_placeholder) {
            _textarea_placeholder = opts.textarea_placeholder;
        }
        if (opts.textarea || opts.textarea_id) {
            var _textareahtml = '<textarea id="' + _textarea_id + '" class="comfim_textarea" placeholder="' + _textarea_placeholder + '"></textarea>';
        }
        var _html = '<div class="comfim_model" id="' + _id + '">' +
            '    <p class="title">' + _title + '</p>' +
            _textareahtml +
            '    <div class="form_btn form_btn_m">' +
            '        <a href="javascript:" class="cancel_btn btn btn_w halfborder">' + (opts.cancel_btn_info ? opts.cancel_btn_info : '取消') + '</a>' +
            '        <a href="javascript:" class="comfim_btn btn halfborder">' + (opts.comfim_btn_info ? opts.comfim_btn_info : '确定') + '</a>' +
            '    </div>' +
            '   <div class="comfim_model_closebtn"></div>' +
            '</div>' +
            '<div id="' + _maskid + '" style="position:fixed;left:0;top:0;bottom:0;right:0;background:rgba(0,0,0,0.7);z-index:3"></div>'
        $('body').append(_html);
        var _modelH = $('#' + _id).height();
        var _oft;
        if (_modelH > $(window).height()) {
            _oft = $('body').scrollTop() + $(window).width() * 0.18
        } else {
            _oft = $('body').scrollTop() + $(window).height() / 2 - _modelH / 2;
        }
        $('#' + _id).css('top', _oft);

        $('#' + _id + ' .comfim_model_closebtn,#' + _id + ' .cancel_btn').on('click', function () {
            $('#' + _id).remove();
            $('#' + _maskid).remove();
        })
        $('#' + _id + ' .comfim_btn').on('click', function () {
            if (opts.comfimBtnCallBack) {
                opts.comfimBtnCallBack();
            }
        })
        $('#' + _id + ' .cancel_btn').on('click', function () {
            if (opts.cancelBtnCallBack) {
                opts.cancelBtnCallBack();
            }
        })
    },
    //可自定义弹窗        
    handleModel: function (_id, _close_btn) {
        $(_id).show();
        OtherPay.MSG.maskDisplay(true);
        var _modelH = $(_id).height();
        var _oft;
        if (_modelH > $(window).height()) {
            _oft = $('body').scrollTop() + $(window).width() * 0.18
        } else {
            _oft = $('body').scrollTop() + $(window).height() / 2 - _modelH / 2;
        }
        $(_id).css('top', _oft);

        var _close_dom

        if (_close_btn) {
            _close_dom = $(_id + ' .handle_model_closebtn ,' + _close_btn);
        } else {
            _close_dom = $(_id + ' .handle_model_closebtn');
        }

        _close_dom.on('click', function () {
            OtherPay.MSG.maskHide();
            $(_id).hide();
            _close_dom.off('click');
        })



    },
    // 服务号公共底部
    footerInit: function () {

        var u = navigator.userAgent;
        var isiOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
        if (isiOS) {
            $('body').addClass('isiOS')
        }

        $('#footer .link_more').on('click', function () {
            $('body').addClass('footer_active')
        })
        $('#footer_mask .link_more').on('click', function () {
            $('body').addClass('footer_mask_out');
            setTimeout(function () {
                $('body').removeClass('footer_active');
                $('body').removeClass('footer_mask_out');
            }, 300)
        })


    },
    //我的工地右侧工地状态4个圆的显示隐藏
    fixedStateBtnInit: function (ishide) {
        if (ishide) {
            $('.fixed_state_btn').removeClass('show')
        }
        $('.fixed_state_btn .btn').on('click', function () {
            if ($('.fixed_state_btn').hasClass('show')) {
                $('.fixed_state_btn').removeClass('show');
            } else {
                $('.fixed_state_btn').addClass('show');
            }
        })
    },
    //公共点击底色变化；
    fuckTheSystemMacruraActive: function () {
        $('body').on('touchstart', '.item_add_active', function () {
            var self = $(this);
            self.addClass('addactive');
            setTimeout(function () {
                self.removeClass('addactive')
            }, 500)
        })
        $('body').on('touchmove touchend', '.item_add_active', function () {
            var self = $(this);
            self.removeClass('addactive');

        });
    },
    /**
    * 简单的右侧栏拖动方法
    * @param  {[type]} _outer 触摸dom
    */
    drapSidePushOver: function (_outer) {
        var self = this;
        _winW = $(window).width();
        _winH = $(window).height();
        _outer.offbottom = parseInt(_outer.css('bottom'));
        _outer.offright = parseInt($(window).width() * 0.03);
        // console.log(_outer.offbottom)
        _outer.minY = _outer.offbottom - 10;
        _outer.moveleftmax = -_winW / 2 + _outer.width();
        _outer.maxY = -(_winH - _outer.height() - _outer.offbottom - 10);
        _outer.on('touchstart', function (e) {

            self.addStopEvent();
            _outer.posX = e.touches[0].pageX - (_outer.nowX || 0);
            _outer.posY = e.touches[0].pageY - (_outer.nowY || 0);
        }, false)

        _outer.on('touchmove', function (e) {

            _outer.nowX = parseInt(e.touches[0].pageX - _outer.posX);
            _outer.nowY = parseInt(e.touches[0].pageY - _outer.posY);

            if (_outer.nowX < -_winW - _outer.width()) {
                _outer.nowX = -_winW + _outer.offright * 2 + _outer.width();
            }
            // console.log(_outer)
            // console.log('translate3d('+ _outer.nowX +'px,'+ _outer.nowY +'px,0)')
            _outer.css({
                transform: 'translate3d(' + _outer.nowX + 'px,' + _outer.nowY + 'px,0)',
                webkitTransform: 'translate3d(' + _outer.nowX + 'px,' + _outer.nowY + 'px,0)'
            })
        }, false)

        _outer.on('touchend touchcancel', function (e) {

            var left, right, paddingLeft, paddingRight;

            if (_outer.nowX < _outer.moveleftmax) {
                _outer.nowX = -_winW + _outer.offright * 3 + _outer.width();
            } else {
                _outer.nowX = 0;
            }

            if (_outer.nowY > _outer.minY) {
                _outer.nowY = _outer.minY
            } else if (_outer.nowY < _outer.maxY) {
                _outer.nowY = _outer.maxY
            }

            _outer.css({
                transition: 'transform 0.3s',
                webkitTransition: 'transform 0.3s',
                transform: 'translate3d(' + _outer.nowX + 'px,' + _outer.nowY + 'px,0)',
                webkitTransform: 'translate3d(' + _outer.nowX + 'px,' + _outer.nowY + 'px,0)',
            });
            _outer.on('webkitTransitionEnd TransitionEnd', function () {
                _outer.css({
                    transition: 'none',
                    webkitTransition: 'none',
                })
            })

            self.removeStopEvent()
        }, false)
    },
    _rightsideBtnOuterChilds: [],
    /**
     * 右侧按钮添加outer
     * @param  {number} opts.order 排序 越小排越上
     * @param  {number} opts.order html 自组件的html
     *
     * order目前排序，如有添加和修改，请在这里维护这份order值
     * 首页按钮 10
     * 搜索按钮 20
     * 返回顶部 30
     * 
     */
    _rightsideBtnOuterInit: function (opts) {
        var self = this;
        var _id = '__rightside_btn_outer';
        var _rightside_btn_outer = '<div id="' + _id + '" class="__rightside_btn_outer"></div>';
        OtherPay.MSG._rightsideBtnOuterChilds.push(opts);
        if ($('#' + _id).length === 0 && $('.fixed_state_btn').length === 0) { // 排除施工阶段
            $('body').append(_rightside_btn_outer);
        } else {
            return;
        }
        $(window).on('load', function () {
            var _outer = $('#' + _id);

            //根据order排序 为了传入的order可以传任意值，所以进行排序
            var arr = OtherPay.MSG._rightsideBtnOuterChilds;
            var _l = arr.length;
            for (var i in arr) {
                for (var j = 0; j < arr.length - 1 - i; j++) {
                    // console.log()
                    if (arr[j].order > arr[j + 1].order) {
                        var _i = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = _i;
                    }
                }
            }
            var _outer_html = '';
            for (var k = 0; k < arr.length; k++) {
                _outer_html += arr[k].html
            }
            _outer.html(_outer_html);


            //右侧栏移动避免遮挡，避免产品因为遮挡的问题反复上下调bottom值
            self.drapSidePushOver(_outer);
        });


    },

   
};


OtherPay.ACT = {
    // 异步提交
    ajaxPost: function (url, queryParams, fun, funError) {
        if (loading) {
            // OtherPay.MSG.maskDisplay();
            OtherPay.MSG.loadingBegin();
        }
        url = OtherPay.Services.Url + url;
        if (isSubmit) {
            isSubmit = false;
        }
        isSubmit = false;
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            data: queryParams || { '__tmp__': '__tmp__' },
            success: function (data) {
                if (fun) {
                    fun(data);
                } else {
                    if (data.Success) {
                        OtherPay.MSG.alert(data.Message);
                    } else {
                        OtherPay.MSG.alert(data.Message);
                    };
                };
                isSubmit = true;
                //OtherPay.MSG.maskHide();
                OtherPay.MSG.loadingEnd();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (funError) {
                    funError();
                }
                else {
                    OtherPay.MSG.alert('数据请求失败');
                }
                isSubmit = true;
                //OtherPay.MSG.maskHide();
                OtherPay.MSG.loadingEnd();
            }
        });
    },
    //滚动加载更多
    asynloadScroll: function (opts) {
        opts = $.extend({
            totalPage: 1,
            url: "",
            data: {},
            pageIndex: 2,
            success: function () { },
            end: function () { },
            error: function () { }
        }, opts || {});
        var pageIndex = opts.pageIndex;

        var loading = true; //正在加载
        $(window).unbind("scroll");
        if (opts.totalPage <= 1) {
            opts.end();
        };
        opts.url = OtherPay.Services.Url + opts.url;
        $(window).scroll(function () {
            var $body = $("body");

            /*判断窗体高度与竖向滚动位移大小相加 是否 超过内容页高度*/
            if (($(window).height() + $(window).scrollTop()) >= $body.height()) {
                if (loading) {
                    loading = false;
                    //$(".load-more").show();
                    //$(".load-more .load-more-btn").text("数据加载中");
                    $(".scroll_more").addClass('loading');
                    opts.data.pageIndex = pageIndex;

                    $.ajax({
                        cache: false,
                        url: opts.url,
                        data: opts.data,
                        success: function (data) {
                            var jsonData = {
                                data: data,
                                pageIndex: pageIndex
                            };
                            opts.success(jsonData);
                            if (pageIndex >= opts.totalPage) {
                                $(window).unbind("scroll");
                                opts.end();
                            }
                            pageIndex++;
                            loading = true;
                        },
                        error: function (e) {
                            opts.error();
                            loading = true;
                        }
                    });
                }
            }
        });
    },
    //异步加载更多
    asynload: function (opts) {
        opts = $.extend({
            totalPage: 1,
            url: "",
            data: {},
            pageIndex: 2,
            success: function () { },
            end: function () { },
            error: function () { },
            obj: ".more-button"
        }, opts || {});

        var pageIndex = opts.pageIndex;

        var loading = true; //正在加载
        if (isNaN(opts.totalPage) || opts.totalPage <= 1) {
            $(opts.obj).attr("end", "true").hide();
            opts.end();
        };
        $(opts.obj).unbind("click");
        opts.url = OtherPay.Services.Url + opts.url;
        $(opts.obj).click(function () {
            if (loading) {
                loading = false;
                opts.data.pageIndex = pageIndex;

                $.ajax({
                    cache: false,
                    url: opts.url,
                    data: opts.data,
                    success: function (data) {
                        var jsonData = {
                            data: data,
                            pageIndex: pageIndex
                        };
                        opts.success(jsonData);
                        if (pageIndex >= opts.totalPage) {
                            $(opts.obj).attr("end", "true").hide();
                            opts.end();
                        }
                        pageIndex++;
                        loading = true;
                    },
                    error: function (e) {
                        opts.error();
                        loading = true;
                    }
                });



            }
        });
    },
    //选择分页
    selectPage: function (opts) {
        opts = $.extend({
            url: "",
            searchForm: "#searchForm",
            success: function () { },
            end: function () { },
            error: function () { },
            obj: ".page_nav"
        }, opts || {});


        var totalPage = $(opts.searchForm + " #totalPage").val()
        var loading = true; //正在加载
        if (isNaN(totalPage) || totalPage <= 1) {
            opts.end();
        };
        opts.url = OtherPay.Services.Url + opts.url;
        //上一页
        $(opts.obj + " .prev").on("click", function () {
            var pageIndex = parseInt($(opts.searchForm + " #pageIndex").val());
            if (loading && pageIndex > 1) {
                loading = false;
                $(opts.searchForm + " #pageIndex").val(pageIndex - 1);
                $.ajax({
                    cache: false,
                    url: opts.url,
                    data: $(opts.searchForm).serialize(),
                    success: function (data) {
                        var jsonData = {
                            data: data,
                            pageIndex: pageIndex
                        };
                        opts.success(jsonData);
                        $(opts.obj + " .select select").val(pageIndex - 1);

                        if (pageIndex <= 1) {
                            opts.end();
                        }
                        loading = true;
                    },
                    error: function (e) {
                        opts.error();
                        loading = true;
                    }
                });


            }
        });
        //下一页
        $(opts.obj + " .next").on("click", function () {
            var pageIndex = parseInt($(opts.searchForm + " #pageIndex").val()) + 1;
            var totalPage = $(opts.searchForm + " #totalPage").val();
            if (loading && pageIndex <= totalPage) {

                loading = false;
                $(opts.searchForm + " #pageIndex").val(pageIndex);
                $.ajax({
                    cache: false,
                    url: opts.url,
                    data: $(opts.searchForm).serialize(),
                    success: function (data) {
                        var jsonData = {
                            data: data,
                            pageIndex: pageIndex
                        };
                        opts.success(jsonData);
                        $(opts.obj + " .select select").val(pageIndex);
                        if (pageIndex >= totalPage) {
                            opts.end();
                        }
                        loading = true;
                    },
                    error: function (e) {
                        opts.error();
                        loading = true;
                    }
                });
            }
        });

        $(opts.obj + " select").on("change", function () {
            pageIndex = $(this).val();
            $(opts.searchForm + " #pageIndex").val(pageIndex);
            var totalPage = $(opts.searchForm + " #totalPage").val();
            loading = false;
            $.ajax({
                cache: false,
                url: opts.url,
                data: $(opts.searchForm).serialize(),
                success: function (data) {
                    var jsonData = {
                        data: data,
                        pageIndex: pageIndex
                    };
                    opts.success(jsonData);
                    $(opts.searchForm + " #pageIndex").val(pageIndex);
                    if (pageIndex == totalPage || pageIndex == 1) {
                        opts.end();
                    }
                    loading = true;
                },
                error: function (e) {
                    opts.error();
                    loading = true;
                }
            });
        });
    },
    picLazyLoad: function () {

        /**
        * Zepto picLazyLoad Plugin
        * ximan http://ons.me/484.html
        * 20140517 v1.0
        */
        ; (function ($) {
            $.fn.picLazyLoad = function (settings) {
                settings = $.extend({ threshold: 0, placeholder: '/Images/nopic.jpg' }, settings || {});
                var $this = $(this), _winScrollTop = settings.threshold, _winHeight = $(window).height();
                $("img[data-original]").addClass("loadBefore");
                lazyLoadPic();
                $(window).on('scroll', function () { _winScrollTop = $(window).scrollTop(); _winHeight = $(window).height(); lazyLoadPic(); });
                function lazyLoadPic() {
                    $this.each(function () {
                        var $self = $(this); if ($self.is('img')) {
                            if ($self.attr('data-original')) {
                                var _offsetTop = $self.offset().top;
                                if ((_offsetTop - settings.threshold) <= (_winHeight + _winScrollTop)) {
                                    $self.attr('src', $self.attr('data-original'));
                                    $self.removeAttr('data-original');
                                    $self.one('load', function () {
                                        $(this).removeClass("loadBefore");
                                        $(this).addClass("beforeEnd");
                                    })
                                }
                            }
                        } else {
                            if ($self.attr('data-original')) {
                                if ($self.css('background-image') == 'none') { $self.css('background-image', 'url(' + settings.placeholder + ')'); }
                                var _offsetTop = $self.offset().top; if ((_offsetTop - settings.threshold) <= (_winHeight + _winScrollTop)) { $self.css('background-image', 'url(' + $self.attr('data-original') + ')'); $self.removeAttr('data-original'); }
                            }
                        }
                    });
                }
            }
        })(Zepto);


        if ($("img[data-original]").length > 0) {
            var set = setInterval(function () {
                var _threshold = 1500;
                if (!_threshold) {
                    _threshold = threshold;
                }
                $("img[data-original]").picLazyLoad({ threshold: _threshold });

                if ($("img[data-original]").length == 0) {
                    clearInterval(set);
                }
            }, 1000);
        }
    },
    ////文件上传
    localResize: function (config) {
        $("#" + config.uploadId).localResizeIMG({
            quality: config.quality,
            ischange: config.ischange,
            success: function (result) {
                var uploadType = config.uploadType;
                var url = "";
                if (config.uploadType == "qyupload") {
                    url = "/QyUploadFile/Index";
                }
                else {
                    url = "/MpUploadFile/Index";
                }
                OtherPay.ACT.ajaxPost(url, { base64: result.base64, buildID: config.buildID }, function (result) {
                    config.callback(result);
                    if (!result.Success && typeof callback != "function") {
                        OtherPay.MSG.alert(result.Message);
                    }
                });
            }
        });
    },

    /**
   * 公共上传多张图片   ,,deleteCb
   * @param  {}       opts.id        外层id
   * @param  {number} opts.maxlength 为最大可上传图片数量 达到上传个数，会自动隐藏上传按钮
   * @param  {fun}    opts.uploadCb  上传之后回调
   * @param  {fun}    opts.deleteCb  删除之后回调 参数为item，可以拿到删除的那个的item
   * @param  {object}    opts.localResizeoption 上传图片配置
   */
    uploadImgChoser: function (opts) {
        var _uic = function () {
            var self = this;
            var _id = opts.id;
            var _outer = $('#' + _id);
            var _uploadbtn = _outer.find('.from_imgsupload_btn').find('input');
            var _uploadbtn_id = _id + '_uploadbtn';
            _uploadbtn.attr('id', _uploadbtn_id)
            //选择图片
            var UPLOADIMG_MAXLENGTH = opts.maxlength ? opts.maxlength : 8;

            var imguplad_html = function (_src) {
                return '<div class="from_imgsupload_item" data-state="add">' +
                    '<div class="from_imgsupload_img">' +
                    '<img src="' + _src + '"  data-preview-src="" data-preview-group="1">' +
                    '</div>' +
                    '<span class="from_imgsupload_del"></span>' +
                    '</div>'
            }

            OtherPay.ACT.localResize({
                uploadId: _uploadbtn_id,
                quality: opts.localResizeoption.quality,
                ischange: opts.localResizeoption.ischange,
                uploadType: opts.localResizeoption.uploadType,
                buildID: opts.localResizeoption.buildID,
                callback: function (result) {
                    console.log(result)
                    if (result.Success) {
                        var base64 = result.Data;
                        var _html = imguplad_html(base64)
                        _outer.prepend(_html)
                        var _length = 0;
                        _outer.find('.from_imgsupload_item').each(function () {
                            if (!$(this).hasClass('from_imgsupload_item_delete')) {
                                _length++
                            }
                        })
                        if (_length === UPLOADIMG_MAXLENGTH) {
                            _uploadbtn.parents('.from_imgsupload_btn').hide()
                        }

                        var _obj = {};
                        _obj.url = base64;
                        _obj.delete = false;
                        if (opts.uploadCb) {
                            opts.uploadCb();
                        }

                    }


                }
            })


            _outer.on('click', '.from_imgsupload_del', function () {
                var _item = $(this).parents('.from_imgsupload_item');
                var _state = _item.attr('data-state');
                if (_state === 'add') {
                    _item.remove();
                } else if (_state === 'edit') {
                    _item.addClass('from_imgsupload_item_delete');
                    _item.attr('data-state', 'del')
                }

                _uploadbtn.parents('.from_imgsupload_btn').show();
                if (opts.deleteCb) {
                    opts.deleteCb(_item);
                }
            })

            //图片base64数组
            self.base64_list = function () {
                //前段的方法
                //var _arr = {};
                //var deletearr = [];
                //var savearr = [];
                //var editdelarr = [];
                //_outer.find('.from_imgsupload_item').each(function () {
                //    var _url = $(this).find('img').attr('src');
                //    var _state = $(this).attr('data-state');
                //    if (_state === 'add') {
                //        savearr.push(_url)
                //    }
                //    else if (_state === 'del') {
                //        editdelarr.push(_url)
                //    }
                //})
                //_arr.delete_arr = deletearr;
                //_arr.save_arr = savearr;
                //_arr.editdelarr = editdelarr;

                //更改后的方法
                var _arr = [];
                _outer.find('.from_imgsupload_item').each(function () {
                    var imgitem = {};
                    imgitem._url = $(this).find('img').attr('src');
                    imgitem.operation = $(this).attr('data-state');
                    imgitem.fid = $(this).attr('data-fid');
                    _arr.push(imgitem);
                })

                return _arr;
            }

        }
        return new _uic(opts);
        //图片放大插件
        mui.previewImage()
    },

    CountDownTime: function (o) { // 获取验证码60秒倒计时
        if (wait == 0) {
            o.removeClass('disabled');
            o.html("重新发送")
            wait = 60;
            oFF = true;
        } else {
            o.addClass('disabled');
            o.html(wait + "s 重新获取 ")
            wait--;
            setTimeout(function () {
                OtherPay.ACT.CountDownTime(o);
            }, 1000);
        }
    },
    //发送短信验证码
    SendMobileCode: function (Mobile, Key, imgKey, imgCode, obj, callback) {
        if (validate.isNull(Mobile)) {
            OtherPay.MSG.alert("请输入手机号", 1000);
            return false;
        }

        if (!validate.isMobile(Mobile)) {
            OtherPay.MSG.alert("手机格式不正确", 1000);
            return false;
        }
        OtherPay.ACT.ajaxPost("/SmS/SendVerificationCode", { pszmobis: Mobile, Key: Key, imgKey: imgKey, imgCode: imgCode }, function (data) {
            if (data.Success) {
                OtherPay.MSG.alert("获取验证码成功,你将在5秒-2分钟内收到验证码!", 1000);
                OtherPay.ACT.CountDownTime(obj);

            } else {
                OtherPay.MSG.alert(data.Message, 1000);
            }
            if (typeof (callback) === 'function') {
                callback(data);
            }
        })
    },
    //根据经纬度定位
    LocationByLatLng: function (callback) {
        $("body").append("<div id='allmap'></div>");
        // 百度地图API功能
        var map = new BMap.Map("allmap");
        var point = new BMap.Point(116.331398, 39.897445);
        map.centerAndZoom(point, 12);

        var geolocation = new BMap.Geolocation();
        geolocation.getCurrentPosition(function (r) {
            if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                var mk = new BMap.Marker(r.point);
                map.addOverlay(mk);
                map.panTo(r.point);
                OtherPay.ACT.ajaxPost("/location/LocationByLatLng/", { lng: r.point.lng, lat: r.point.lat }, function (data) {
                    if (callback) {
                        callback(data);
                    }
                });
            }
            else {
                if (callback) {
                    var result = {};
                    result.Success = false;
                    result.Message = this.getStatus();
                    callback(result);
                }
            }
        }, { enableHighAccuracy: true })

    },
    //根据经纬度获取附近地址
    GetLocationByLatLng: function (callback, pois) {
        $("body").append("<div id='allmap'></div>");
        // 百度地图API功能
        var map = new BMap.Map("allmap");
        var point = new BMap.Point(116.331398, 39.897445);
        map.centerAndZoom(point, 12);
        if (pois == undefined) {
            pois = 0;
        }
        var geolocation = new BMap.Geolocation();
        geolocation.getCurrentPosition(function (r) {
            if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                var mk = new BMap.Marker(r.point);
                map.addOverlay(mk);
                map.panTo(r.point);
                OtherPay.ACT.ajaxPost("/location/GetLocationByLatLng/", { lng: r.point.lng, lat: r.point.lat, pois: pois }, function (data) {
                    if (callback) {
                        callback(data);
                    }
                });
            }
            else {
                if (callback) {
                    var result = {};
                    result.Success = false;
                    result.Message = this.getStatus();
                    callback(result);
                }
            }
        }, { enableHighAccuracy: true })

    },
    ajaxComplete: function (callback) {
        $(document).ajaxComplete(function (event, jqXHR, options) {
            OtherPay.MSG.loadingEnd(callback);
        });
    },
    ajaxBeforeSend: function (callback) {
        $(document).ajaxSend(function (event, jqXHR, options) {
            OtherPay.MSG.loadingBegin(callback);
        });
    }
};


OtherPay.FN = {
    in_array: function (search, arr) {
        // 遍历是否在数组中 
        var count = arr.length;
        for (var i = 0, k = count; i < k; i++) {
            if (search == arr[i]) {
                return true;
            }
        }
        // 如果不在数组中就会返回false 
        return false;
    },
    remove_array: function (search, arr) {
        arr.splice($.inArray(search, arr), 1);
        return arr;
    },
    htmlspecialchars: function (str) {
        if (!str) { return };
        str = str.replace(/&/g, '&amp;');
        str = str.replace(/</g, '&lt;');
        str = str.replace(/>/g, '&gt;');
        str = str.replace(/"/g, '&quot;');
        str = str.replace(/'/g, '&#039;');
        return str;
    },
    htmlspecialchars_decode: function (str) {
        if (!str) { return };
        str = str.replace(/&amp;/g, '&');
        str = str.replace(/&lt;/g, '<');
        str = str.replace(/&gt;/g, '>');
        str = str.replace(/&quot;/g, '"');
        str = str.replace(/&#039;/g, "'");
        return str;
    },
    getQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    },
    buildForm: function (url, queryParams, method) {
        var form = $("<form>");//定义一个form表单
        form.attr("style", "display:none");
        form.attr("target", "");
        form.attr("method", method || 'post');
        form.attr("action", url);
        $("body").append(form);//将表单放置在web中
        $.each(queryParams, function (index, val) {
            var input = $("<input>");
            input.attr("type", "hidden");
            input.attr("name", val.key);
            input.attr("value", val.value);
            form.append(input);
        });
        return form;
    },
    formatterBool: function (cellvalue, options, rowObject) {
        var temp = ""
        switch (cellvalue) {
            case 1:
                temp = "<font color='red'>是</font>";
                break;
            case 0:
                temp = "<font color='green'>否</font>";
                break;
        }
        return temp;
    },
    renderTime: function (data) { //格式化日期/json时间转化('/Date(1216796600500)/'    >>   '2013-08-01 00:00:00'  )
        var da = eval('new ' + data.replace('/', '', 'g').replace('/', '', 'g'));
        return da.getFullYear() + "-" + OtherPay.FN.completion((da.getMonth() + 1), 2) + "-" + OtherPay.FN.completion(da.getDate(), 2) + "  " + OtherPay.FN.completion(da.getHours(), 2) + ":" + OtherPay.FN.completion(da.getMinutes(), 2) + ":" + OtherPay.FN.completion(da.getSeconds(), 2);
    },
    completion: function (str, len) { //长度补全
        var result = str + "";
        var strLen = result.length;
        if (strLen < len) {
            var zone = '';
            for (var i = strLen; i < len; i++) {
                zone += "0";
            }
            return zone + result;
        } else {
            return result;
        }
    }
};
$(function () {

    //验证是否为验证小数
    var val = "";
    $("input[type='number']").on("keyup", function (e) {
        if (validate.isFloat2($(this).val())) {
            val = $(this).val();
        }
        if ($(this).val() == "") {
            val = "";
        }
        $(this).val(val);
    });

    OtherPay.ACT.picLazyLoad();
})


//#region 数据验证
var validate = {
    isInteger: function (val) {//验证正整数，包括0
        var patten = /^[1-9]\d*|0$/;
        return patten.test(val);
    },

    isIdcard: function (val) {//验证身份证 
        var patten = /^\d{15}(\d{2}[A-Za-z0-9])?$/;
        return patten.test(val);
    },

    idPhone: function (val) {//验证电话号码 
        var patten = /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/;
        return patten.test(val);

    },

    isMobile: function validateNum(val) {// 验证手机号码 
        var patten = /^(13|14|15|16|17|18|19)\d{9}$/;
        return patten.test(val);

    },

    isTelephone: function (val) { //验证手机或电话号
        var patten = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1})|(0[0-9]{2,3}))+\d{7,8})$/;
        return patten.test(val);
    },

    isEmail: function (val) {//验证email账号
        var patten = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        return patten.test(val);
    },

    isNum: function (val) {//验证整数
        var patten = /^-?\d+$/;
        return patten.test(val);
    },

    isRealNum: function (val) {//验证实数 
        var patten = /^-?\d+\.?\d*$/;
        return patten.test(val);

    },

    isFloat2: function validateNum(val) {//验证小数，保留2位小数点 
        var patten = /^[0-9]+([.]{1}[0-9]{0,2})?$/;
        return patten.test(val);

    },

    isFloat: function (val) {//验证小数
        var patten = /^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/;
        return patten.test(val);
    },

    isNumOrLetter: function (val) {//只能输入数字和字母
        var patten = /^[A-Za-z0-9]+$/;
        return patten.test(val);
    },

    isColor: function (val) {//验证颜色
        var patten = /^#[0-9a-fA-F]{6}$/;
        return patten.test(val);
    },

    isUrl: function (val) { //验证URL
        var patten = /^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i;
        return patten.test(val);
    },

    isNull: function (val) {//验证空
        if (!val) {
            return true;
        }
        return val.replace(/\s+/g, "").length == 0;
    },

    isData: function (val) {//验证时间2010-10-10
        var patten = /^\d{4}[\/-]\d{1,2}[\/-]\d{1,2}$/;
        return patten.test(val);
    },

    isNumLetterLine: function (val) {//只能输入数字、字母、下划线
        var patten = /^[a-zA-Z0-9_]{1,}$/;
        return patten.test(val);
    },

    changeDateFormat: function (jsondate, formart) {
        if (jsondate && jsondate != '') {
            jsondate = jsondate.replace("/Date(", "").replace(")/", "");
        } else {
            return '';
        }
        if (jsondate.indexOf("+") > 0) {
            jsondate = jsondate.substring(0, jsondate.indexOf("+"));
        } else if (jsondate.indexOf("-") > 0) {
            jsondate = jsondate.substring(0, jsondate.indexOf("-"));
        }
        var date = new Date(parseInt(jsondate, 10));
        if (isNaN(date.getMonth())) {
            return '';
        }
        if (!formart || formart == '') {
            formart = "yyyy-MM-dd HH:mm:ss";
        }
        var o = {
            "M+": date.getMonth() + 1,
            "d+": date.getDate(),
            "H+": date.getHours(),
            "m+": date.getMinutes(),
            "s+": date.getSeconds(),
            "q+": Math.floor((date.getMonth() + 3) / 3),
            "S": date.getMilliseconds()
        };
        if (/(y+)/.test(formart)) {
            formart = formart.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.legth));
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(formart)) {
                formart = formart.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
        return formart;
    }
}
//#endregion 数据验证
