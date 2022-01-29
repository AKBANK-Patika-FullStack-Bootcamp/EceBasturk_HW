const girlsPowerSum = (number) => {
    return (number/2) + 3;
}

const arr = [2,3,4]

const girlsPower = (arr, func) => arr.map((value) => func(value));

console.log(`Eski diziniz [${arr}] girlsPower ile degistirildi.\nYeni diziniz:`, girlsPower(arr, girlsPowerSum));

