/*
    Legibilidade => Código claro e fácil de entender
*/

let preco = 19.90;
let desconto = 0.4;
let precoComDesconto = preco * (1 - desconto);

console.log(19.9 * 0.6); // 11.94
console.log(preco * (1 - desconto)); // 11.94
console.log(precoComDesconto); // 11.94

let nome = "Caderno";
let categoria = "Papelaria";
console.log("Produto: " + nome + ", Categoria: " + categoria + ", Preço: " + preco + ", Desconto: " + desconto + ", Preço com Desconto: " + precoComDesconto); // Produto: Caderno, Categoria: Papelaria, Preço: 19.9, Desconto: 0.4, Preço com Desconto: 11.94