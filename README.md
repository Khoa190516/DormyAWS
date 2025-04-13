# 🚀 Deploy Docker Compose từ GitHub lên EC2 (Amazon Linux)

Hướng dẫn từng bước để clone project từ GitHub và deploy ứng dụng sử dụng Docker Compose trên EC2.

---

## ✅ Yêu cầu

- Tài khoản AWS
- EC2 instance (Amazon Linux 2 hoặc Ubuntu)
- Key `.pem` để SSH
- Project có `Dockerfile` và `docker-compose.yml`

---

## 🛠 1. Tạo EC2 Instance

- Launch EC2 với Amazon Linux 2
- Tạo Security Group mở các cổng:
  - `22` (SSH)
  - `80` (HTTP)
  - `443` (HTTPS - nếu dùng)
  - Các cổng khác nếu cần (ví dụ: `5000`, `5174`,...)

---

## 💻 2. SSH vào EC2

```bash
ssh -i your-key.pem ec2-user@<EC2-IP>
```

---

## 🔧 3. Cài Git, Docker & Docker Compose

### Cài Git

```bash
sudo yum update -y
sudo yum install git -y
```

### Cài Docker

```bash
sudo amazon-linux-extras enable docker
sudo yum install docker -y
sudo service docker start
sudo usermod -aG docker ec2-user
```

➡️ Đăng xuất và SSH lại để áp dụng quyền `docker`.

### Cài Docker Compose

```bash
sudo curl -L "https://github.com/docker/compose/releases/download/v2.22.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose
docker-compose --version
```

---

## 📦 4. Clone project từ GitHub

```bash
git clone https://github.com/yourusername/yourrepo.git
cd yourrepo
```

---

## ⚙️ 5. Cấu hình app (nếu cần)

- Sửa file `.env`
- Kiểm tra giá trị `VITE_BASE_API_URL`, `DATABASE_URL`,...
- Đảm bảo Dockerfile và Docker Compose build đúng context

---

## 🚀 6. Build & chạy Docker Compose

```bash
docker-compose up -d --build
```

Kiểm tra container đang chạy:

```bash
docker ps
```

---

## 🌐 7. Reverse Proxy với Nginx (trong container)

**File cấu hình Nginx** (ví dụ: `nginx/default.conf`):

```nginx
server {
    listen 80;
    server_name yourdomain.com;

    location / {
        proxy_pass http://web:80;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }

    location /api/ {
        proxy_pass http://api:80/;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

---

## 🌍 8. Trỏ domain bằng Route 53 (nếu có)

- Tạo Hosted Zone trên Route53
- Tạo bản ghi A trỏ về IP public của EC2
- Kiểm tra bằng `http://yourdomain.com`

---

## 🔐 9. (Optional) Cài HTTPS với Let's Encrypt

```bash
sudo yum install certbot python3-certbot-nginx -y
sudo certbot --nginx
```

---

## 🎉 DONE!

Truy cập:
- http://<EC2-IP>
- hoặc http://yourdomain.com
