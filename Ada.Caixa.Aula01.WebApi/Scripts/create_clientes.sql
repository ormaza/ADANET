create table Clientes
(
    Id integer not null primary key autoincrement,
    Nome text not null,
    Email text unique,
    DataCriacao text default current_timestamp
)