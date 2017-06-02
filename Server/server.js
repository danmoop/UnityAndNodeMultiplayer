var io = require('socket.io')(process.env.Port || 1337);
var shortId = require('shortid');

console.log('server started');

var player = {
    x: 0,
    y: 0
}

io.on('connection', function(socket) {


    socket.emit('spawn', player);

    console.log('connected');

    socket.on('moveUpRequest', function() {
    	player.y = player.y + 0.2;
        socket.emit('setPos', player);
        socket.broadcast.emit('setPos', player);
        console.log(player);
    });

    socket.on('moveDownRequest', function() {
    	player.y = player.y - 0.2;
        socket.emit('setPos', player);
        socket.broadcast.emit('setPos', player);
        console.log(player);
    });

    socket.on('moveLeftRequest', function() {
    	player.x = player.x - 0.2;
        socket.emit('setPos', player);
        socket.broadcast.emit('setPos', player);
        console.log(player);
    });

    socket.on('moveRightRequest', function() {
    	player.x = player.x + 0.2;
        socket.emit('setPos', player);
        socket.broadcast.emit('setPos', player);
        console.log(player);
    });

    socket.on('clearChat', function(){
    	for(var i = 0; i < 30; i++){
    		console.log('');
    	}
    	console.log('------------------------------------------------------------------------');
    	console.log('');
    });

    socket.on('showDebug', function(){
        console.log(player);
    });

    socket.on('disconnect', function() {
        console.log('disconnected');
    });
});