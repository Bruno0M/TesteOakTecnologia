<h1 align="center" style="font-weight: bold;">Teste Oak Tecnologia 💻</h1>

<p align="center">
 <a href="#technologies">Tecnologias</a> • 
 <a href="#started">Vamos Começar!</a> • 
</p>

<p align="center">
    <b>Projeto referente a vaga de estágio da Oak Tecnologia </b>
</p>

<h2 id="technologies">💻 Tecnologias</h2>

#### Front-End:

- React
- TypeScript
- Vite
- MaterialUI

#### Back-End:

- C#
- ASP.NET 8
- JWT (Json Web Token)
- Entity Framework
- Swagger
- PostgreSQL
- pgadmin4

#### Devops
- Docker
- Docker Compose

<h2 id="started">🚀 Vamos Começar!</h2>

<h3>Pré-Requisitos</h3>

Para rodar o projeto, é necessário que tenha:

- Docker
- Node

<h3>Clonando o Projeto</h3>

```bash
git clone https://github.com/Bruno0M/TesteOakTecnologia.git
```

<h3>Rodando o Projeto</h3>

Para rodar nosso FrontEnd, basta seguir o seguinte:
```bash
cd TesteOakTecnologia/TesteOakTecnologiaWeb/
npm install
npm run dev
```
Com isso, o Front-End estará disponível localmente em `http://localhost:5173`

</br>

E para rodar a Api do Projeto:
```bash
cd TesteOakTecnologia/TesteOakTecnologiaAPI/
docker-compose up -d
```
Em seguida, você vai ter a API Rodando em na porta `1111`, e caso queira ver a documentação da API, basta acessar com o Swagger `http://localhost:1111/swagger/index.html`.

A porta `8002` é onde está rodando o banco de dados, e caso queira administrar esse banco, basta acessar o `pgadmin4` em `http://localhost:16543/login`