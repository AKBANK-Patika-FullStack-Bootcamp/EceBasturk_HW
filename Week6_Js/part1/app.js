const {zoro} = require('./dog.js');
const  {CleanDog, dogCareTime } = require('./dogCareUtility.js');

console.log(`Kopek adi: ${zoro.name}\nKopegin cinsi: ${zoro.breed}`);
  
console.log(CleanDog());
console.log(`Kopek ilgi saati: ${dogCareTime * zoro.weight}`);