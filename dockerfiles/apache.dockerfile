##########################################################################################################

FROM ubuntu/apache2:latest

#update/upgrade packages
RUN apt-get update -y && apt-get upgrade -y


#install fpm dependencies
RUN apt-get install libapache2-mod-fcgid -y

#enable apache modules as defined in .env file
ARG APACHE_EXTENSIONS=${APACHE_EXTENSIONS}
RUN a2enmod ${APACHE_EXTENSIONS}
##########################################################################################################