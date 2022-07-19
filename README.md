# DOCKER PHP-DEV ENVIRONMENT

## Disclaimer
This container is not intended for productive use as everything is done using the root user. It poses a high security risk using this container in production!

## What it does
This Docker container sets up a basic DEV environment for PHP Development. It uses **apache2** as the webserver and **php-fpm** for handling php scripts. **Domains** are handled via wildcard.

A **cli** is also included with **php, composer and node.js / npm** installed.
 
It uses **MariaDB** on port 3306 for access with prefered db client.
  
**Phpmyadmin** is included on port 8080 to access the database from browser without external db client.

## Used Images
>- ubuntu/apache2:latest
>- php:fpm
>- php:cli (including node.js)
>- mariadb:latest
>- phpmyadmin:latest

## Usage
1. Rename **.env.sample** file to **.env** and change the contents as desired.

**Constants used in .env file:**
>- **PROJECT DIRECTORY**
>The relative path to the directory containing all your projects. 
>- **PHP_VERSION**
> The desired PHP version, uncomment desired version, leave rest commented.
>- **PHP_EXTENSIONS**
> The PHP extensions that should be installed on image build. Enter space separated. If new modules are added after initial build process, repeat build process using `docker compose build php` and `docker compose build cli`
>- **MYSQL_DATABASE (optional)**
> Database that gets created on container creation
>- **MYSQL_USER (optional)**
> Database user with read/write access for MYSQL_DATABASE
>- **MYSQL_PASSWORD (optional)**
> Password for MYSQL_USER
>- **MYSQL_ROOT_PASSWORD**
> Password for mysql root user
>- **BASH_EXTENSIONS**
> The os extensions that should be installed for use in cli and fpm. Enter space separated. If new extensions are added after initial build process, repeat build process using `docker compose build php` or `docker compose build cli`
>- **NODE_VERSION**
> The desired NODE version, enter "node" to install latest version else enter version in format xx.xx.x
>- **APACHE_EXTENSIONS**
> The desired apache modules which should be enabled. Enter space separated. If new modules are added after initial build process, repeat build process using `docker compose build web`
Required Modules: vhost_alias rewrite proxy_fcgi

2. Use `docker compose up -d` to start the container. On first start docker will start pulling and building the images. This process will take a lot of time so be patient. If the **PHP_VERSION** is changed in the **.env** file the build process will repeat on first start. After first start, switching **PHP_VERSION** will be way faster.

3. All your projects will be reachable via wildcard domain, the pattern is **foldername.test** if you wish to change the domain ending it has to be changed in **./data/apache/wildcard.conf** on line 2

4. There are multiple ways to reach wildcard domains:
    1. Edit your local host file and add entries in the following pattern:
        > 127.0.0.1 foldername.test
        > ::1 foldername.test
    2. (Recommended)
    Use a service like **dnsmasq** for mac/linux or **acrylic dns** for windows to support wildcard dns. (look up tutorials for usage)
        > dnsmasq for mac
        > https://formulae.brew.sh/formula/dnsmasq
        > dnsmasq for ubuntu
        > https://wiki.ubuntuusers.de/Dnsmasq/
        > acrylic dns for windows
        > https://mayakron.altervista.org/support/acrylic/Home.htm

5. **ssh, git** and **composer** config for **php-fpm** and **cli** will be mounted from host home directory to container root user directory so the files need to be created and stored on host first.

6. **php.ini** changes can be made in **./dockerfiles/php.ini**, rebuild is required afterwards using `docker compose build php` and `docker compose build cli`

7. **xdebug.ini** changes can be made in **./data/php/xdebug.ini**, no rebuild required, only restart.

8. **MariaDB Database** can be accessed with external database management tool like **dbeaver** and alike using **localhost:3306** as host. If you don't wish to use an external database tool you can use **phpmyadmin** in your browser by visiting **localhost:8080**. Databases will be persisted in **./databases** folder.

9. To access the **bash cli** run the following command to start a cli container which closes itself after exiting: `docker compose run --rm cli`