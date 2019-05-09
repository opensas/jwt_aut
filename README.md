# Proyecto de ejemplo de autenticación con jwt

## Objetivo

El presente proyecto tiene por objetivo servir como referencia para autenticarse con una API rest utilizando JWT. A su vez se utilizará para efectuar pruebas de conectividad.

## Instalación

- Instalar netcore 2.2.203 ([descargar aquí](https://dotnet.microsoft.com/download/dotnet-core/2.2))

- Descargar el repositorio

```shell
$ git clone git@gitlab.com:sgte-it/trabajo/fiscalizacion/jwt_auth.git
```

- Descargar las dependencias y ejecutar el proyecto

```shell
$ cd jwt_auth/src

$ dotnet restore
  Restore completed in 764.58 ms for /home/sas/devel/apps/sgte-it/trabajo/fiscalizacion/tmp/jwt_auth/src/src.csproj.

$ dotnet run
: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0]
      User profile is available. Using '/xxxxxxx/.aspnet/DataProtection-Keys' as key repository; keys will not be encrypted at rest.
Hosting environment: Development
Content root path: /xxxxxxxxxx/jwt_auth/src
Now listening on: http://localhost:4000
Application started. Press Ctrl+C to shut down.
```

## Obtención de un JWT (Json Web Token)

Usuario: "produccion-api-username"

Password = "produccion-api-test-Lj1KzZii28"

Para obtener un JWT hay que efectuar un POST al endpoint /apiRoot/users/authenticate, de la siguiente manera

```shell
$ curl -X POST http://localhost:4000/users/authenticate \
>   -H "Content-Type: application/json" \
>   -H "Accept: application/json" \
>   -d '{"Username": "produccion-api-username","Password": "produccion-api-test-Lj1KzZii28"}'

{
  "id":2,
  "firstName":"Produccion",
  "lastName":"API",
  "username":"produccion-api-username",
  "password":null,"token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJuYmYiOjE1NTY3ODkwNzEsImV4cCI6MTU1NzM5Mzg3MSwiaWF0IjoxNTU2Nzg5MDcxfQ.2vk-C83XRgI0CwNPXR1_zjsOTzk3OCVt2j_esdGxNqw"
}
```

Sintaxis de curl de windows

```shell
curl -X POST http://localhost:4000/users/authenticate ^
  -H "Content-Type: application/json" ^
  -H "Accept: application/json" ^
  -d "{\"Username\": \"produccion-api-username\",\"Password\": \"produccion-api-test-Lj1KzZii28\"}"

{
  "id":2,
  "firstName":"Produccion",
  "lastName":"API",
  "username":"produccion-api-username",
  "password":null,"token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJuYmYiOjE1NTY3ODkwNzEsImV4cCI6MTU1NzM5Mzg3MSwiaWF0IjoxNTU2Nzg5MDcxfQ.2vk-C83XRgI0CwNPXR1_zjsOTzk3OCVt2j_esdGxNqw"
}
```

## Acceder a un endpoint protegido utilizando el JWT

Utilizando el token obtenido en el ejemplo anterior, pasarlo en un header de tipo Authorization

```shell
$ curl -X GET http://localhost:4000/info \
>   -H "Accept: application/json" \
>   -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJuYmYiOjE1NTY3ODkwNzEsImV4cCI6MTU1NzM5Mzg3MSwiaWF0IjoxNTU2Nzg5MDcxfQ.2vk-C83XRgI0CwNPXR1_zjsOTzk3OCVt2j_esdGxNqw"

{
  "http":{
    "is_https":false,
    "method":"GET",
    "path":"/info",
    "path_base":"",
    "protocol":"HTTP/1.1",
    "scheme":"http",
    },
  },
  "ip":{
    "remote_ip":"127.0.0.1",
    "remote_port":54846,
    "local_ip":"127.0.0.1",
    "local_port":4000
  },
  "platform":{
    "c#":"v4.0.30319",
    "runtime":".NETCoreApp,Version=v2.2",
    "netcore":"2.2.4",
    "framework":".NET Core 2.2.4 (Framework 4.6.27521.02)",
    "framework_version":"4.6.27521.02"
  },
  "os":{
    "description":"Linux 4.15.0-1036-oem #41-Ubuntu SMP Sun Apr 7 05:24:01 UTC 2019",
    "architecture":"X64",
    "process_architecture":"X64",
    "server":""
  }
}
```

Sintaxis de curl de windows

```shell
curl -X GET http://localhost:4000/info ^
  -H "Accept: application/json" ^
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjIiLCJuYmYiOjE1NTY3ODkwNzEsImV4cCI6MTU1NzM5Mzg3MSwiaWF0IjoxNTU2Nzg5MDcxfQ.2vk-C83XRgI0CwNPXR1_zjsOTzk3OCVt2j_esdGxNqw"
```

Para más ejemplos de uso ver el archivo `docs/examples.rest`