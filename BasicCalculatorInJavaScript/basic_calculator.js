/**
 * A basic calculator, first project in JavaScript
 * 
 * @author Sarthak Jain
 */

document.getElementById('headerTag').innerHTML = "Type of calculation"



var num1 = prompt("Enter the first number")
var num2 = prompt("Enter the second number")
var sign = prompt("Enter the type of calculation (+,-,*,/)")

calculation(num1, num2, sign)

function calculation(num1, num2, sign){
    try {
        
        var newNum1 = parseFloat(num1);
        var newNum2 = parseFloat(num2);
        if(isNaN(newNum1) || isNaN(newNum2)){
            throw "Number entered is not valid"
        }
        num1 = newNum1
        num2 = newNum2
        if(sign == "+"){
            var newNum = num1 + num2
            document.getElementById('headerTag').innerHTML = "Addition"
            document.getElementById('paragraphTag').innerHTML = num1 + " + " + num2 + " = " +  newNum
            alert("Your calculation led to: " + newNum)
        }else if(sign == "-"){
            var newNum = num1 - num2
            document.getElementById('headerTag').innerHTML = "Subtraction"
            document.getElementById('paragraphTag').innerHTML = num1 + " - " + num2 + " = " +  newNum
            alert("Your calculation led to: " + newNum)
        } else if (sign == "*") {
            var newNum = num1 * num2
            document.getElementById('headerTag').innerHTML = "Multiplication"
            document.getElementById('paragraphTag').innerHTML = num1 + " * " + num2 + " = " +  newNum
            alert("Your calculation led to: " + newNum)
        }else if(sign == "/"){
            var newNum = num1 / num2
            document.getElementById('headerTag').innerHTML = "Division"
            document.getElementById('paragraphTag').innerHTML = num1 + " / " + num2 + " = " +  newNum
            alert("Your calculation led to: " + newNum)
        }else{
            if(!sign.includes("+") || !sign.includes("-") || !sign.includes("*") || !sign.includes("/")){
                throw "Sign entered is invalid"
            }
            if(sign.length > 1){
                throw "There is more than one sign entered"
            }
            alert("Error, your operator is invalid")
        }           
    } catch (error) {
        alert(error)        
    }
}