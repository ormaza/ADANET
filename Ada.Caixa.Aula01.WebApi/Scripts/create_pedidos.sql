create table Pedidos
(
    Id integer primary key autoincrement,
    Descricao text,
    ClienteId integer,
    foreign key (ClienteId) references Clientes(Id)
)