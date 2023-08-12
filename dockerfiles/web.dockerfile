ARG PHP_VERSION=${PHP_VERSION}

##########################################################################################################
FROM php:${PHP_VERSION}-apache

#update/upgrade packages
RUN apt-get update -y && apt-get upgrade -y

#enable apache modules as defined in .env file
ARG APACHE_EXTENSIONS=${APACHE_EXTENSIONS}
RUN a2enmod ${APACHE_EXTENSIONS}
##########################################################################################################