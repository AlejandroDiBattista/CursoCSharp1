
var a = [1, 2, 3, 4, 5, 6, 7];

var doble = x => x * x;
var par   = x => x % 2;

console.log( a.filter(par).map(doble) );