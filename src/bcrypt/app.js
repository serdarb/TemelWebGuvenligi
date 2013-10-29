var express = require('express'),
	mongoose = require('mongoose'),
    bcrypt  = require('bcrypt-nodejs');

mongoose.connect('mongodb://localhost/bcryptdemo');
var user = mongoose.model('User', 
						   mongoose.Schema({ email: String, name: String, pass: String }));

var app = express();
app.listen(3000);
app.use('/s', express.static('s'));
app.use(express.favicon('s/img/favicon.ico')); 
app.use(express.bodyParser());
app.use(express.cookieParser());
app.use(express.session({secret:"notsosecret"}));

var authChecker = function(req, res, next){
	if (req.session.user 
		&& req.session.user.loggedIn) {
	    next();
	}
	else {
	    res.redirect('/login');
	}	
}

app.get('/', authChecker, function(req, res) { res.sendfile('./views/index.html'); });

app.get('/signup', function(req, res) {	res.sendfile('./views/signup.html'); });
app.get('/login', function(req, res) { res.sendfile('./views/login.html'); });

app.post('/signup', function(req, res) {
	
	var name = req.body.name.trim();
	var email = req.body.email.trim().toLowerCase();
	var pass = req.body.pass.trim();

	var salt = bcrypt.genSaltSync(12);
	var hash = bcrypt.hashSync(pass, salt);

	var newUser = new user({ email: email, name: name, pass: hash});
	newUser.save(function (err, result) {
		req.session.user = {loggedIn : true, email : email, name:name};
		res.redirect('/');
	});

    res.sendfile('./views/failed.html');
});


app.post('/login', function(req, res) {

	var email = req.body.email.trim().toLowerCase();
	var pass = req.body.pass.trim();

	user.findOne({email: email}, function(err, item) {		

		if (bcrypt.compareSync(pass, item.pass)) {
			
			req.session.user = {loggedIn : true, email : email, name:item.name};
			res.redirect('/');
		};        
    }); 
  
    res.sendfile('./views/failed.html');
});

app.use(function(req, res){ res.send('Hello'); });

String.prototype.trim=function(){return this.replace(/^\s+|\s+$/g, '');};

