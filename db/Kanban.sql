CREATE TABLE "Tarea"(
	"id" INTEGER NOT NULL,
	"id_tablero" INTEGER NOT NULL,
	"nombre" TEXT NOT NULL,
	"estado" INTEGER NOT NULL,
	"descripcion" TEXT,
	"color" TEXT,
	"id_usuario_asignado" INTEGER,
	PRIMARY KEY("id" AUTOINCREMENT),
	FOREIGN KEY("id_tablero") REFERENCES Tablero("id")
);