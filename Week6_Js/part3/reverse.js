const reverseFunc1 = (str) => {
    return str.split("").reverse().join("")
};

const reverseFunc2 = (str) => {
    if(str === "") return "";
    else return reverseFunc2(str.substr(1)) + str.charAt(0);
};

//reverserFunc3 ve reverseFunc4 aynı time complexityde işlem yaptığını ve aynı oranda bellek harcadığını düşünüyorum.
const reverseFunc3 = (str) => {
    let reverse = [];
    for(let i of str){
        reverse.unshift(i);
    }
    return reverse.join("");
};

function reverseFunc4(str){
    let reverse = [];
    for(let i= str.length; i--;i>0){
        reverse[str.length-i] = str[i];
    }
    return reverse.join("");
}
console.log(reverseFunc1("Reverse"));
console.log(reverseFunc2("String"));
console.log(reverseFunc3("Javascript"));
console.log(reverseFunc4("Example"));

