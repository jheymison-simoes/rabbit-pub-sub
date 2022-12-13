<h1>Projeto .NET com Exemplo Pub SUb com Rabbit Mq</h1>

Neste projeto, foi criado exemplo completo de como publicar e consumir filas no Rabbit MQ. Foi utilizado container docker que exevuta o Rabbit.

<h3> Como Executar este projeto? </h3>

<li>Execute o Docker para descktop</li>
<li>Utilizando o terminal, navegue até a página raiz do projeto e execute o comando: docker-compose -f .\docker-maintenance-vehicle-block\docker-compose.yml up</li>
<li>No Docker Descketop, observe se foram criados dois containers rabbit-api e rabbit-pub-rabbit</li>
<li>Verifique se os dois foram exevutados, caso não, execute-os</li>
<li>Abra o navegador e cole o ling http://localhost:5015/swagger</li>
<li>Execute o unico endpoint que contém</li>
<li>Observe o console, será printada a mensagem!</li>
<li>Caso queira observar o trafego do publish no rabbitMq, abra no navegador http://localhost:5678, e observe os graficos</li>

<h3>Explicação</h3>
<p>
    Ao exevutar o projeto é feita a conexão com o rabbitMq. Quando o endpoint é chamado, publica a mensagem na fila. Há um consumidor que fica ouvindo a fila e printa a mensagem no console.
</p>