<? 
 require_once("$_SERVER[DOCUMENT_ROOT]/../includes/flight/Flight.php");
 
 function MyFirstProc() {
		echo "Hello from REST";
	}
	
 Flight::route("GET /myfirstproc","MyFirstProc");
	
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