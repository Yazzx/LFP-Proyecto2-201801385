using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2
{
    class ErrorLéxico
    {
        int id, fila, columna;

        String descripción;
        char caracter;

        public ErrorLéxico(int id, int fila, int columna, char caracter, string descripción)
        {
            this.id = id;
            this.fila = fila;
            this.columna = columna;
            this.caracter = caracter;
            this.descripción = descripción;
        }

        public int getColumna()
        {
            return columna;
        }
        public void setColumna(int nuevoo)
        {
            this.columna = nuevoo;
        }
        public int getFila()
        {
            return fila;
        }
        public void setFila(int nuevoo)
        {
            this.fila = nuevoo;
        }
        public int getId()
        {
            return id;
        }
        public void setId(int nuevoo)
        {
            this.id = nuevoo;
        }
        public String getDescripción()
        {
            return descripción;
        }

        public void setDescripción(String newName)
        {
            this.descripción = newName;
        }

        public char getCaracter()
        {
            return caracter;
        }

        public void setCaracter(char newName)
        {
            this.caracter = newName;
        }

        public String toString()
        {
            return "ObjError: { id= " + id + ", fila= " + fila + ", columna= " + columna +
                ", caracter= " + caracter + ", descripción= " + descripción + " }";
        }


    }
}

