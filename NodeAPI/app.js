var express = require('express');
var bodyParser = require('body-parser');
var mysql = require('mysql');
const uuidv4 = require('uuid/v4');
const PORT = process.env.PORT || 5000

var app = express();

var jsonParser = bodyParser.json();

//create sql connection
var connection = mysql.createConnection({
    host:"sulnwdk5uwjw1r2k.cbetxkdyhwsb.us-east-1.rds.amazonaws.com",
    user:"qmekkwvqaxgyo2bp",
    password:"mgjlji6mur6uepn2",
    database: 'sphkbr79eaqoz3xa'
})



//Select List of skills
app.get('/SkillsSelect',function(req,res){
    connection.query("CALL skills_select()", function(err,result){
    res.json(result[0]);
    });   
});

//Create a new skill
app.post('/SkillsInsert',jsonParser, function(req,res){
    connection.query("CALL skills_insert('"+ uuidv4() +"','"+ req.body.SkillType +"','"+ req.body.SkillName +"','"+ req.body.Image +"',"+ req.body.Enabled +",'TBD')",function(err,result){
        res.json(err);
    });
});

//Update skill State
app.post('/SkillsUpdate', jsonParser,function(req,res){
    connection.query("CALL skills_enabledupdate('"+ req.body.SkillId +"',"+ req.body.Enabled +")",function(err,result){
        res.json(err);
    });
});

//Delete Skill
app.post('/SkillsDelete',jsonParser,function(req,res){
    connection.query("CALL skills_delete('"+ req.body.SkillId +"')",function(err,result){
        res.json(err);
    });
});

app.listen(5000);