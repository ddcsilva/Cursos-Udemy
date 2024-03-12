/*
    var => É uma palavra reservada para criar variáveis
    let => É uma palavra reservada para criar variáveis
*/

var nome = "Caneta";
var quantidade = 10;
var preco = 6.4;
let imposto = 1.5;
let precoFinal = preco + imposto;

console.log(nome);
console.log(quantidade);
console.log(preco);
console.log(imposto);
console.log(precoFinal);

nome = "Caneta BIC";
console.log(nome);

blabla = 123; // Não é uma boa prática criar variáveis sem a palavra reservada var
console.log(blabla);