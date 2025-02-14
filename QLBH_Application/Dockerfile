# Sử dụng hình ảnh Node.js chính thức
FROM node:18-alpine AS build
WORKDIR /app

# Copy package.json và cài đặt dependencies
COPY package*.json ./
RUN npm install

# Copy toàn bộ mã nguồn và build
COPY . .
RUN npm run build

# Dùng Nginx để phục vụ file tĩnh
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]