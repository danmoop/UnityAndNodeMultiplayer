var express = require('express');
var app = express();
var server = require('http').createServer(app);
var io = require('socket.io').listen(server);

server.listen(process.env.PORT || 1337);

var users = [];

function Player(x,y,id){
    this.x = x;
    this.y = y;
    this.id = id;
}

console.log('Server Started(1)');

io.on('connection', function(socket) {

    var player = new Player(0,0,socket.id);
    users.push(player);

    console.log(users);
    socket.broadcast.emit('spawn', player);

    for(var playerId in users){
        if(playerId == socket.id)
            continue;
        socket.emit('spawn', users[playerId]);
    }

    socket.on('disconnect', function() {
        console.log('disconnected');
        socket.emit('close', player);
        users.splice(player);
    });

    socket.on('moveUpRequest', function(){
        player.y = player.y + 0.2;
        socket.emit('setPos', player);
        socket.broadcast.emit('setPos', player);
    });

    socket.on('moveDownRequest', function(){
        player.y = player.y - 0.2;
        socket.emit('setPos', player);
        socket.broadcast.emit('setPos', player);
    });

    socket.on('moveLeftRequest', function(){
        player.x = player.x - 0.2;
        socket.emit('setPos', player);
        socket.broadcast.emit('setPos', player);
    });

    socket.on('moveRightRequest', function(){
        player.x = player.x + 0.2;
        socket.emit('setPos', player);
        socket.broadcast.emit('setPos', player);
    });
});