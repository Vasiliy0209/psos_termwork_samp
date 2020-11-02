<? 
 require_once("$_SERVER[DOCUMENT_ROOT]/../includes/flight/Flight.php");
 require_once("$_SERVER[DOCUMENT_ROOT]/../db/dal.inc.php");
 
 function CreateLanguage() {
	 //file_put_contents("log.txt",var_export(Flight::request()->data["Name"],TRUE));
	 DBCreateLanguage(Flight::request()->data["Name"]);
 }
 Flight::route('PUT /rest/language',"CreateLanguage");
 
 function ListLanguages() {
		//echo "Hello from REST";
		Flight::json(DBListLanguages());
	}
	
 Flight::route("GET /rest/languages","ListLanguages");
 
 function ReadLanguage($id) {
		Flight::json(DBReadLanguage($id));
 }
 
 Flight::route('GET /rest/language\?id=@id',"ReadLanguage");
 
 function UpdateLanguage($id) {
	 DBUpdateLanguage($id,
		Flight::request()->data["Name"]
	 );
 }
 Flight::route('PATCH /rest/language\?id=@id',"UpdateLanguage");
 
 function DeleteLanguage($id) {
	 DBDeleteLanguage($id);
 }
 Flight::route('DELETE /rest/language\?id=@id',"DeleteLanguage");
 //--------------
 function CreatePerson() {
	 DBCreatePerson(
		Flight::request()->data["Name"],
		Flight::request()->data["Age"],
		Flight::request()->data["Mail"],
		Flight::request()->data["LanguageID"]
	);
 }
 Flight::route('PUT /rest/person',"CreatePerson");
 
 function ListPeople() {
		//echo "Hello from REST";
		Flight::json(DBListPeople());
	}
	
 Flight::route("GET /rest/people","ListPeople");
 
 function ReadPerson($id) {
		//echo "Hello from REST";
		Flight::json(DBReadPerson($id));
	}
	
 Flight::route("GET /rest/person\?id=@id","ReadPerson");
 
 function UpdatePerson($id) {
	 DBUpdatePerson(
		$id,
		Flight::request()->data["Name"],
		Flight::request()->data["Age"],
		Flight::request()->data["Mail"],
		Flight::request()->data["LanguageID"]		
	);
 }
 Flight::route('PATCH /rest/person\?id=@id',"UpdatePerson");
 
 function DeletePerson($id) {	 
	 DBDeletePerson($id);
 }
 Flight::route('DELETE /rest/person\?id=@id',"DeletePerson");
 /*Flight::route('GET /wall\.post\.xml\?resp=@resp',function($resp) {
	if($resp=="ok")
	{
		echo "<response><post_id>1</post_id></response>";
	}
	else
	{
		echo "<error><error_code>2</error_code><error_msg>Hello</error_msg></error>";
	}
 });
 
 function Hello() {
	echo "Hello, RESTfull World !!!";
 }
 
 Flight::route('GET /hello',"Hello");
 
 //Аргументы в стартовой строке
 Flight::route('GET /myapi\?arg1=@val1&arg2=@val2',function($val1,$val2) {
	echo "Получены аргументы: arg1=[$val1] arg2=[$val2]";
 });
 
 //Аргументы в теле запроса
 Flight::route('POST /myapi_post',function() {	
	$arg1=Flight::request()->data->arg1;
	$arg2=Flight::request()->data->arg2;
	//header('Access-Control-Allow-Origin: 123');
	echo "POST1 Получены аргументы:".
	" arg1=".$arg1.
	" arg2=".$arg2;
	//var_dump(Flight::request());
 });
 
 //Аргументы в теле запроса
 Flight::route('PUT /myapi',function() {	
	$arg1=Flight::request()->data->arg1;
	$arg2=Flight::request()->data->arg2;
	//header('Access-Control-Allow-Origin: 123');
	echo "PUT Получены аргументы:".
	" arg1=".$arg1.
	" arg2=".$arg2;
	//var_dump(Flight::request());
 });
 
Flight::route('GET /\?method=flickr.interestingness.getList&api_key=@apiKey&extras=@extras', function($apiKey,$extras){    
	echo "<?xml version=\"1.0\"?>\n";
	?><photos page="1">
		<photo id="1" title="MyPhoto1"/>
		<photo id="2" title="MyPhoto2"/>
	  </photos>
	<?	
});

Flight::route('GET /\?myxml', function(){    
	echo "<?xml version=\"1.0\"?>\n";
	?><photos page="1">
		<photo id="1" title="MyPhoto1"/>
		<photo id="2" title="MyPhoto2"/>
	  </photos>
	<?	
	header('Access-Control-Allow-Origin: null');
});

Flight::route('GET /\?myjson', function(){
    echo "{\"resourceName\": \"demo\",\"a\":\"4\"}";	
});*/

 Flight::start();
?>