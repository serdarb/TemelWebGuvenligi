var express = require('express');
var app = express();

app.listen(3000);

app.use('/s', express.static('s'));
app.use(express.favicon('s/img/favicon.ico')); 
app.use(express.bodyParser());

app.get('/', function(req, res) { res.sendfile('./views/index.html'); });
app.get('/signup', function(req, res) { res.sendfile('./views/signup.html'); });
app.get('/login', function(req, res) { res.sendfile('./views/login.html'); });

app.post('/signup', function(req, res) {

	

  
});


app.post('/login', function(req, res) {

	

  
});

app.use(function(req, res){ res.send('Hello'); });