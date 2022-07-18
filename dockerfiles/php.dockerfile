#use php version from .env file
ARG PHP_VERSION=${PHP_VERSION}
ARG TYP=${TYP}

##########################################################################################################

## BASE IMAGE
FROM php:${PHP_VERSION}-${TYP} as base
#use php/bash extensions as defined .env file
ARG PHP_EXTENSIONS=${PHP_EXTENSIONS}
ARG BASH_EXTENSIONS=${BASH_EXTENSIONS}

#install bash extensions as defined in .env file
RUN apt-get install ${BASH_EXTENSIONS} -y

#allow git inside container
ENV GIT_DISCOVERY_ACROSS_FILESYSTEM=1

#set additional ini directory/copy php.ini
ENV PHP_INI_SCAN_DIR=/usr/local/etc/php/conf.d:/usr/local/etc/php/conf.d/php_ini_files
COPY php.ini /usr/local/etc/php/php.ini

#add php extension installer
ENV IPE_LZF_BETTERCOMPRESSION=1
ENV IPE_GD_WITHOUTAVIF=1
ADD https://github.com/mlocati/docker-php-extension-installer/releases/latest/download/install-php-extensions /usr/local/bin/

RUN chmod +x /usr/local/bin/install-php-extensions && \
    mkdir -p /usr/local/etc/php/conf.d/php_ini_files && \
    install-php-extensions ${PHP_EXTENSIONS}

#update/upgrade packages
RUN apt-get update -y && apt-get upgrade -y

##########################################################################################################

##CLI ADDITION
FROM base as cli

#use node version as defined in .env file
ARG NODE_VERSION=${NODE_VERSION}

#install nvm/node/npm as defined in .env file
RUN curl https://raw.githubusercontent.com/creationix/nvm/master/install.sh | bash \
&& . ~/.bashrc \
&& nvm install ${NODE_VERSION}

#select workdir and start bash shell on container execution
WORKDIR /var/www/html
CMD bash

##########################################################################################################

FROM base as fpm

##########################################################################################################
