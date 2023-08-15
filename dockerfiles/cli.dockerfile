ARG PHP_VERSION=${PHP_VERSION}

##########################################################################################################

FROM php:${PHP_VERSION}-cli

#update/upgrade packages
RUN apt-get update -y && apt-get upgrade -y

#install desired bash extensions
ARG BASH_EXTENSIONS=${BASH_EXTENSIONS}
RUN apt-get install ${BASH_EXTENSIONS} -y

#include php extension installer script because image provided method sucks balls
ADD https://github.com/mlocati/docker-php-extension-installer/releases/latest/download/install-php-extensions /usr/local/bin/
RUN chmod +x /usr/local/bin/install-php-extensions

#install node/npm via nvm
ARG NODE_VERSION=${NODE_VERSION}
RUN curl https://raw.githubusercontent.com/creationix/nvm/master/install.sh | bash \
&& . ~/.bashrc \
&& nvm install ${NODE_VERSION}

#create new user
ARG USERNAME=$USERNAME
RUN adduser $USERNAME 

#move and change permission for required npm/nvm files
RUN mv /root/.npm /home/$USERNAME/
RUN mv /root/.nvm /home/$USERNAME/
RUN chown $USERNAME:$USERNAME /home/$USERNAME/.npm
RUN chown $USERNAME:$USERNAME /home/$USERNAME/.nvm

#move and change permission from root user to new user
RUN rm /home/$USERNAME/.bashrc
RUN mv /root/.bashrc /home/$USERNAME/.bashrc
RUN chown $USERNAME:$USERNAME /home/$USERNAME/.bashrc

#install desired php extensions
ARG PHP_EXTENSIONS=${PHP_EXTENSIONS}
RUN install-php-extensions ${PHP_EXTENSIONS}

#select workdir and start bash shell on container execution
WORKDIR /var/www/html
CMD bash

##########################################################################################################