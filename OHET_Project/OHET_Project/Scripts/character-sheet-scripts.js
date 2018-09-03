$('.tabs .tab').click(function () {
    if ($(this).hasClass('main')) {
        $('.tabs .tab').removeClass('active');
        $(this).addClass('active');
        $('.cont').hide();
        $('.main-cont').show();
    }
    if ($(this).hasClass('actions')) {
        $('.tabs .tab').removeClass('active');
        $(this).addClass('active');
        $('.cont').hide();
        $('.actions-cont').show();
    }
    if ($(this).hasClass('feats')) {
        $('.tabs .tab').removeClass('active');
        $(this).addClass('active');
        $('.cont').hide();
        $('.feats-cont').show();
    }
    if ($(this).hasClass('bg')) {
        $('.tabs .tab').removeClass('active');
        $(this).addClass('active');
        $('.cont').hide();
        $('.bg-cont').show();
    }
    if ($(this).hasClass('equip')) {
        $('.tabs .tab').removeClass('active');
        $(this).addClass('active');
        $('.cont').hide();
        $('.equip-cont').show();
    }
});
//$('.container .bg').mousemove(function(e){
//    var amountMovedX = (e.pageX * -1 / 30);
//    var amountMovedY = (e.pageY * -1 / 9);
//    $(this).css('background-position', amountMovedX + 'px ' + amountMovedY + 'px');
//});


var actions = new Vue({
    el: '#actions',
    data: {
        items: [
            { title: 'Thing 1', desc: 'Foo' },
            { title: 'Thing 2', desc: 'Bar' }
        ]
    }
})

var bactions = new Vue({
    el: '#bonus-actions',
    data: {
        items: [
            { title: 'Thing 1', desc: 'Foo' },
            { title: 'Thing 2', desc: 'Bar' }
        ]
    }
})

var options = new Vue({
    el: '#options',
    data: {
        items: [
            { title: 'Thing 1', desc: 'Foo' },
            { title: 'Thing 2', desc: 'Bar' }
        ]
    }
})