function validate (){
    document.getElementById("searchError").innerHTML = "";

    let status = true;

    if (document.getElementById("search").value == ""){
        document.getElementById("searchError").innerHTML = "Phase Can Not Be Null";
		document.getElementById("searchError").style.color = "red";
        document.getElementById("searchError").style.fontSize = "14px";
        status = false;
    }
    else if (document.getElementById("searchError").value != ""){
        document.getElementById("searchError").innerHTML = "Phase Can Not Be Null";
		document.getElementById("searchError").style.color = "blue";
        document.getElementById("searchError").style.fontSize = "14px";
        status = false;
    }
}

function clean (){
    document.getElementById("searchError").innerHTML = "";
}