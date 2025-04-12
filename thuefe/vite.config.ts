import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import dotenv from "dotenv";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  define: {
    "process.env.VITE_BASE_API_URL": JSON.stringify(
      process.env.VITE_BASE_API_URL
    ),
  },
  server: {
    host: true,
    strictPort: true,
    port: 5174,
    origin: "http://0.0.0.0:5174",
    watch: {
      usePolling: true,
    },
  },
});
