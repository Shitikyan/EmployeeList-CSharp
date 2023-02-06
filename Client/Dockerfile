FROM node:16-alpine 

WORKDIR /app

COPY . .

RUN npm ci 

RUN npm run build

ENV NODE_ENV production
EXPOSE 80

CMD [ "npx", "serve", "-p", "80", "build" ]