## Stage 1 - Build
FROM node:17.4.0-alpine as build
WORKDIR /usr/src/app
COPY package.json package-lock.json ./
# Install the dependencies
RUN npm install
# Copy the files
COPY . ./
RUN npm run build

## Stage 2: deploy
FROM nginx:1.21.5-alpine
# Copy Nginx config file
COPY ./docker/nginx/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist/frontend /usr/share/nginx/html
