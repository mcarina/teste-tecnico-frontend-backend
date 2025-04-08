FROM node:lts-alpine

WORKDIR /app

COPY package*.json ./

RUN npm install -g @angular/cli && npm install --force

COPY . .

EXPOSE 8080

CMD ["npm", "start"]
