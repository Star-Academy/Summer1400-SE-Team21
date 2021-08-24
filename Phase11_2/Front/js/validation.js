function validate (){
    document.getElementById("searchError").innerHTML = "";

    let status = true;

    if (document.getElementById("search").value == ""){
        document.getElementById("searchError").innerHTML = "Phase Can Not Be Null";
		document.getElementById("searchError").style.color = "red";
        document.getElementById("searchError").style.fontSize = "14px";
        status = false;
    }
    else if (document.getElementById("search").value.length > 2){
        const xmlhttp = new XMLHttpRequest();
        xmlhttp.onload = function() {
            document.getElementById("searchError").style.color = "blue";
            document.getElementById("searchError").style.fontSize = "14px";
            const myArr = (this.responseText).split("\"");
            for (let i = 0; i<myArr.length; i++){
                if (i % 2 == 1){
                    document.getElementById("searchError").innerHTML += myArr[i] + "<br>";
                }
                if (i > 20){
                    break;
                }
            }
        }
        xmlhttp.open("GET", "https://localhost:5001/Search/GetHints?hint=" + document.getElementById("search").value);
        xmlhttp.send();
    }
}

function clean (){
    document.getElementById("searchError").innerHTML = "";
}