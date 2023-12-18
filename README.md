# DOCKER PHP-DEV ENVIRONMENT

## Disclaimer
This container is not intended for productive use, as it was not checked for security flaws and should therefore only be used for development purposes.

## What it does
This Docker container sets up a basic DEV environment for PHP/JS Development. It uses **apache2** as the webserver. **Domains** are handled via wildcard.

A **cli** is also included with **php, composer and node.js / npm** installed.
 
It uses **MariaDB** on port 3306 for access with a prefered db client.
  
**PhpMyAdmin** is included on port 8080 to access the database from browser without external db client.

## Used Images
>- ubuntu:apache2
>- php:fpm
>- php:cli
>- mariadb:latest
>- phpmyadmin:latest

## Usage
1. Rename **.env.sample** file to **.env** and change the contents as desired.

**Constants used in .env file:**
>- **WORKDIR**
>The relative path to the directory containing all your projects. 
>- **PHP_VERSION**
> The desired PHP version for the cli container, uncomment desired version, leave rest commented.
>- **PHP_EXTENSIONS**
> The PHP extensions that should be installed on image build. Enter space separated. If new modules are added after initial build process, repeat build process using `docker compose build web` and `docker compose build cli`
>- **MYSQL_DATABASE (optional)**
> Database that gets created on container creation
>- **MYSQL_USER (optional)**
> Database user with read/write access for MYSQL_DATABASE
>- **MYSQL_PASSWORD (optional)**
> Password for MYSQL_USER
>- **MYSQL_ROOT_PASSWORD**
> Password for mysql root user
>- **BASH_EXTENSIONS**
> The os extensions that should be installed for use in cli and fpm. Enter space separated. If new extensions are added after initial build process, repeat build process using `docker compose build cli`
>- **NODE_VERSION**
> The desired NODE version, enter "node" to install latest version else enter version in format xx.xx.x
>- **APACHE_EXTENSIONS**
> The desired apache modules which should be enabled. Enter space separated. If new modules are added after initial build process, repeat build process using `docker compose build web`
Required Modules: vhost_alias rewrite proxy_fcgi
>- **USERNAME**
> The desired username which will be used in the cli image
>- **UID**
> The userID which will be used in the cli image (should be 1000 in most cases)
>- **GID**
> The groupID which will be used in the cli image (should be 1000 in most cases)
>- **IPV4**
> The desired IPv4 address used in the cli image. Can range from 192.168.2.0 to 192.168.2.

2. Use `docker compose --build` this will start pulling and building the images. This process will take a lot of time so be patient. If the **PHP_VERSION** is changed in the **.env** file the build process for the cli container needs to be repeated. This step is only needed once for every **PHP_VERSION** if you need them.

3. If you need to switch the server php-fpm version, create an empty file in your project root with one of the following names: 80.phpversion / 81.phpversion / 82.phpversion no restart required.

4. Once the images are built, the container can be started with `docker compose up -d`.

5. All your projects will be reachable via wildcard domain, the pattern is **foldername.localhost** to reach the **root** of your project or **foldername.public.localhost** to reach the **public** folder of your project, if you wish to change the domain ending it has to be changed in **./data/apache/wildcard.conf** on line 2

6. Domains ending in .localhost always loop back to 127.0.0.1 so there is no need for a dns service like dnsmasq.

7. **.ssh** and **.gitconfig** for the **cli** image will be mounted from the host home directory to the container user home directory as defined in the .env file. So the files need to be created and stored on the host first.

8. **php.ini** changes can be made in **./data/php/php.ini**, the container needs to be restarted afterwards with `docker compose restart`.

9. **xdebug.ini** rename either **xdebug.ini.unix** for Mac and Linux or **xdebug.ini.wsl2** for Windows WSL2 to **xdebug.ini** in the **./data/php** directory, the container needs to be restarted afterwards with `docker compose restart`.

    1. If you use WSL2, you need to take a few more extra steps to make step debugging work, check the following [gist](https://gist.github.com/Patrick-Gugelsberger/b3ec9453007bb33227472d75e192eaa2) for instructions.
    2. Enter the chosen nameserver to your **xdebug.ini** file in the **xdebug.client_host** field. 

10. **MariaDB Database** can be accessed with external database management tool like **dbeaver** and alike using **localhost:3306** as host. If you don't wish to use an external database tool you can use **phpmyadmin** in your browser by visiting **localhost:8080**. Databases will be persisted in **./databases** folder.

11. To access the **bash cli** run the following command to start a cli container which closes itself after exiting: `docker compose run --rm cli`