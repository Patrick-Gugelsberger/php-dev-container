ARG PHP_VERSION=${PHP_VERSION}

##########################################################################################################
FROM php:${PHP_VERSION}-apache

#update/upgrade packages
RUN apt-get update -y && apt-get upgrade -y

#include php extension installer script because image provided method sucks balls
ADD https://github.com/mlocati/docker-php-extension-installer/releases/latest/download/install-php-extensions /usr/local/bin/
RUN chmod +x /usr/local/bin/install-php-extensions

#install required php extensions
ARG PHP_EXTENSIONS=${PHP_EXTENSIONS}
RUN install-php-extensions ${PHP_EXTENSIONS}

#enable apache modules as defined in .env file
ARG APACHE_EXTENSIONS=${APACHE_EXTENSIONS}
RUN a2enmod ${APACHE_EXTENSIONS}
##########################################################################################################