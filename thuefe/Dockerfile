# Use an official Node.js image as the base image
FROM node:18-alpine AS build

# Set the working directory inside the container
WORKDIR /app

# Copy package.json and yarn.lock to the container
COPY package.json yarn.lock ./

# Install dependencies
RUN yarn install

# Copy the rest of the application code to the container
COPY . .

ARG VITE_BASE_API_URL
ENV VITE_BASE_API_URL=$VITE_BASE_API_URL
RUN echo "VITE_BASE_API_URL=$VITE_BASE_API_URL"

# Build the Vite project
RUN yarn build

# Use a lightweight web server to serve the built files
FROM nginx:alpine AS production

# Copy the built files from the previous stage to the Nginx web server directory
COPY --from=build /app/dist /usr/share/nginx/html

# Expose the port Nginx will run on
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]