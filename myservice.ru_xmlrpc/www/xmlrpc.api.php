<?php
	require_once("$_SERVER[DOCUMENT_ROOT]/../db/dal.inc.php");
	require_once("XML/RPC/Server.php");
	require_once("XML/RPC.php");	
	
	//--------Person,People-------------------------------
	function CreatePerson($params)
	{
		$struct = $params->getParam(0)->getval();
		
		DBCreatePerson(
			$struct["Name"],
			$struct["Age"],
			$struct["Mail"],
			$struct["LanguageID"]
		);
		
		return new XML_RPC_Response(
			new XML_RPC_Value(_DBInsertID(),"int")
		);
	}
	
	function ListPeople()
	{		
		$people=DBListPeople();
		
		foreach($people As $k=>$person) {
			$people[$k]["ID"]=(int)$person["ID"];
			$people[$k]["Age"]=(int)$person["Age"];
			$people[$k]["LanguageID"]=(int)$person["LanguageID"];
		}
		
		return new XML_RPC_Response(
			XML_RPC_encode($people)
		);
	}
	
	function ReadPerson($params)
	{		
		$id = $params->getParam(0)->getval();
		
		$person=DBReadPerson($id);
		
		$person["ID"]=(int)$person["ID"];
		$person["Age"]=(int)$person["Age"];
		$person["LanguageID"]=(int)$person["LanguageID"];
		
		return new XML_RPC_Response(
			XML_RPC_encode(
				$person
			)
		);
	}
	
	function UpdatePerson($params)
	{	
		$id = $params->getParam(0)->getval();
		$struct = $params->getParam(1)->getval();
		DBUpdatePerson(
			$id,
			$struct["Name"],
			$struct["Age"],
			$struct["Mail"],
			$struct["LanguageID"]
		);
		
		return new XML_RPC_Response(
			new XML_RPC_Value(true,"boolean")
		);
	}
	
	function DeletePerson($params)
	{	
		$id = $params->getParam(0)->getval();
		DBDeletePerson($id);
		return new XML_RPC_Response(
			new XML_RPC_Value(true,"boolean")
		);
	}
	//--------Languages----------------------------
	function CreateLanguage($params)
	{
		$struct = $params->getParam(0)->getval();
		DBCreateLanguage($struct["Name"]);
		
		return new XML_RPC_Response(
			new XML_RPC_Value(_DBInsertID(),"int")
		);
	}
	
	function ListLanguages()
	{
		$languages=DBListLanguages();
		//Приведение значений, полученных из БД
		//к их правильным типам данных.
		//По умолчанию все значения, полученные из
		//БД имеют тип string.
		foreach($languages As $k=>$language)
			$languages[$k]["ID"]=(int)$language["ID"];
		
		return new XML_RPC_Response(
			XML_RPC_encode($languages)
		);
	}	
	
	function ReadLanguage($params)
	{			
		$id = $params->getParam(0)->getval();
		
		$language=DBReadLanguage($id);
		$language["ID"]=(int)$language["ID"];
		
		return new XML_RPC_Response(
			XML_RPC_encode(
				$language
			)
		);
	}
	
	function UpdateLanguage($params)
	{			
		$id = $params->getParam(0)->getval();
		$struct = $params->getParam(1)->getval();
		
		DBUpdateLanguage($id,$struct["Name"]);
		
		return new XML_RPC_Response(
			new XML_RPC_Value(true,"boolean")
		);
	}
	
	function DeleteLanguage($params)
	{			
		$id = $params->getParam(0)->getval();
		DBDeleteLanguage($id);
		
		return new XML_RPC_Response(
			new XML_RPC_Value(true,"boolean")
		);
	}
	
	$map=Array(
			"myservice:CreatePerson"=>Array(
				"function"=>"CreatePerson",
				"signature"=>Array(
					Array("int","struct")
				)
			),
			"myservice:ListPeople"=>Array(
				"function"=>"ListPeople",
				"signature"=>Array(
					Array("array")
				)
			),
			"myservice:ReadPerson"=>Array(
				"function"=>"ReadPerson",
				"signature"=>Array(
					Array("struct","int")
				)
			),
			"myservice:UpdatePerson"=>Array(
				"function"=>"UpdatePerson",
				"signature"=>Array(
					Array("boolean","int","struct")
				)
			),
			"myservice:DeletePerson"=>Array(
				"function"=>"DeletePerson",
				"signature"=>Array(
					Array("boolean","int")
				)
			),
			//---------------------------------------
			"myservice:CreateLanguage"=>Array(
				"function"=>"CreateLanguage",
				"signature"=>Array(
					Array("int","struct")
				)
			),
			
			"myservice:ListLanguages"=>Array(
				"function"=>"ListLanguages",
				"signature"=>Array(
					Array("array")
				)
			),
			"myservice:ReadLanguage"=>Array(
				"function"=>"ReadLanguage",
				"signature"=>Array(
					Array("struct","int")
				)
			),
			"myservice:UpdateLanguage"=>Array(
				"function"=>"UpdateLanguage",
				"signature"=>Array(
					Array("boolean","int","struct")
				)
			),
			"myservice:DeleteLanguage"=>Array(
				"function"=>"DeleteLanguage",
				"signature"=>Array(
					Array("boolean","int")
				)
			)			
		);
	
	$srv=new XML_RPC_Server($map,1,1);
