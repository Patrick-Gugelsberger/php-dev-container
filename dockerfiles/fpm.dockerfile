ARG PHP_VERSION=${PHP_VERSION}

##########################################################################################################

FROM php:${PHP_VERSION}-fpm

#update/upgrade packages
RUN apt-get update -y && apt-get upgrade -y

#include php extension installer script because image provided method sucks balls
ADD https://github.com/mlocati/docker-php-extension-installer/releases/latest/download/install-php-extensions /usr/local/bin/
RUN chmod +x /usr/local/bin/install-php-extensions

#install desired php extensions
ARG PHP_EXTENSIONS=${PHP_EXTENSIONS}
RUN install-php-extensions ${PHP_EXTENSIONS}

##########################################################################################################