FROM nginx:stable-alpine AS base
RUN rm -rf /usr/share/nginx/html/*
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

FROM node:20-alpine AS build
WORKDIR /app

COPY package.json yarn.lock ./
RUN yarn install --frozen-lockfile

COPY . .
RUN yarn build

FROM base AS final
COPY --from=build /app/dist /usr/share/nginx/html
