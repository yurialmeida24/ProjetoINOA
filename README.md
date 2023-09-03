# ProjetoINOA

Programa que avisa, por email caso a cotação de um ativo da B3 caia mais do que certo nível, ou suba acima de outro.

## Instruções de Uso

1) O programa é uma aplicação de console que deve ser chamado pela linha de comando com três parâmetros, respectivamente: 
O ativo e em seguida os preços de referência de venda e compra. Como o exemplo a seguir: inoaProjeto.exe PETR4 22,59 22,67
Atenção: deve-se usar vírgula para as casas decimais nos preços de referência.

2) No arquivo de configuração (config.json) só se deve inserir o email do destinatário, onde há a indicação.
Os outros dados já foram fornecidos, podendo mudar a escolha do usuário.

## Notas Importantes

1) A API escolhida foi de https://brapi.dev/
2) A API é atualizada de 15 em 15 minutos, desde que o mercado esteja aberto.
 


   
