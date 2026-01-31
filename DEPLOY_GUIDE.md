# Hướng dẫn Deploy PCMSystem lên VPS

## Thông tin kết nối
- **IP VPS**: 103.77.172.155
- **User**: root
- **Domain**: http://daovietem.duckdns.org
- **Token DuckDNS**: 1d2c8661d-3333-477e-a2c7-3099494e956c

---

## Bước 1: Kết nối SSH vào VPS

Mở PowerShell hoặc CMD trên máy và chạy:

```powershell
ssh root@103.77.172.155
```
Nhập mật khẩu root khi được yêu cầu.

---

## Bước 2: Cài đặt các thành phần cần thiết trên VPS

Chạy các lệnh sau trên VPS (từng dòng một):

```bash
# Cập nhật hệ thống
apt update && apt upgrade -y

# Cài đặt .NET SDK 10.0
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 10.0 --install-dir /usr/share/dotnet
ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

# Cài đặc các công cụ bổ sung
apt install -y nginx curl wget git nano unzip htop

# Cài đặt Certbot cho SSL
apt install -y certbot python3-certbot-nginx

# Kiểm tra .NET đã cài đặt
dotnet --version
```

---

## Bước 3: Cấu hình DuckDNS (Dynamic DNS)

```bash
# Tạo thư mục cho DuckDNS
mkdir -p /opt/duckdns
cd /opt/duckdns

# Tạo script cập nhật DNS
cat > duckdns.sh << 'EOF'
#!/bin/bash
echo url="https://www.duckdns.org/domains/daovietem.duckdns.org/update?token=1d2c8661d-3333-477e-a2c7-3099494e956c" | curl -k -K -
EOF

chmod +x duckdns.sh

# Tạo systemd service cho DuckDNS
cat > /etc/systemd/system/duckdns.service << 'EOF'
[Unit]
Description=DuckDNS Dynamic DNS Updater
After=network-online.target
Wants=network-online.target

[Service]
Type=oneshot
ExecStart=/opt/duckdns/duckdns.sh
RemainAfterExit=yes

[Install]
WantedBy=multi-user.target
EOF

# Tạo timer để cập nhật mỗi 5 phút
cat > /etc/systemd/system/duckdns.timer << 'EOF'
[Unit]
Description=Run DuckDNS updater every 5 minutes

[Timer]
OnBootSec=1min
OnUnitActiveSec=5min
Persistent=true

[Install]
WantedBy=timers.target
EOF

# Kích hoạt DuckDNS service
systemctl daemon-reload
systemctl enable duckdns.service
systemctl enable duckdns.timer
systemctl start duckdns.timer
systemctl start duckdns.service

# Kiểm tra
systemctl status duckdns.service
```

---

## Bước 4: Publish ứng dụng trên máy local

Trên máy của bạn (Windows), chạy lệnh sau trong thư mục dự án:

```powershell
dotnet publish -c Release -r linux-x64 --self-contained false -o C:\Users\Admin\Desktop\BAIKIEMTRA2\kiemtra2\PCMSystem\publish
```

Hoặc nếu muốn tạo file nén để upload:

```powershell
# Tạo thư mục publish
dotnet publish -c Release -r linux-x64 --self-contained false -o C:\Users\Admin\Desktop\BAIKIEMTRA2\kiemtra2\PCMSystem\publish

# Nén thư mục publish (sử dụng 7-Zip hoặc PowerShell)
# Cách 1: Dùng tar (có sẵn trên Windows 10+)
tar -czvf C:\Users\Admin\Desktop\BAIKIEMTRA2\kiemtra2\PCMSystem\pcmsystem.tar.gz -C C:\Users\Admin\Desktop\BAIKIEMTRA2\kiemtra2\PCMSystem\publish .
```

---

## Bước 5: Upload code lên VPS

**Cách 1: Sử dụng SCP (trên PowerShell/CMD):**

```powershell
# Upload file nén
scp C:\Users\Admin\Desktop\BAIKIEMTRA2\kiemtra2\PCMSystem\pcmsystem.tar.gz root@103.77.172.155:/root/

# Upload database nếu cần
scp "C:\Users\Admin\Desktop\BAIKIEMTRA2\kiemtra2\PCMSystem\pcmsystem.db" root@103.77.172.155:/root/
```

**Cách 2: Sử dụng WinSCP** (giao diện đồ họa - khuyến nghị cho người mới)
- Tải WinSCP từ https://winscp.net
- Kết nối với Host: 103.77.172.155, User: root
- Kéo thả file từ máy local vào /root/

---

## Bước 6: Cấu hình trên VPS

```bash
# Di chuyển đến thư mục gốc
cd /root

# Giải nén code
tar -xzvf pcmsystem.tar.gz -C /var/www/pcmsystem/
mkdir -p /var/www/pcmsystem

# Tạo thư mục cho database và copy database
mkdir -p /var/www/pcmsystem/data
cp /root/pcmsystem.db /var/www/pcmsystem/data/pcmsystem.db
chmod 755 /var/www/pcmsystem/data/pcmsystem.db

# Thiết lập quyền
chown -R www-data:www-data /var/www/pcmsystem
chmod -R 755 /var/www/pcmsystem

# Di chuyển vào thư mục ứng dụng
cd /var/www/pcmsystem
```

---

## Bước 7: Cấu hình Nginx

```bash
# Tạo file cấu hình Nginx
cat > /etc/nginx/sites-available/pcmsystem << 'EOF'
server {
    listen 80;
    server_name daovietem.duckdns.org;

    # Ghi log
    access_log /var/log/nginx/pcmsystem_access.log;
    error_log /var/log/nginx/pcmsystem_error.log;

    # Cấu hình proxy cho ASP.NET Core
    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;
    }
}
EOF

# Kích hoạt cấu hình
ln -s /etc/nginx/sites-available/pcmsystem /etc/nginx/sites-enabled/
rm /etc/nginx/sites-enabled/default  # Xóa default config nếu có

# Kiểm tra cấu hình Nginx
nginx -t

# Khởi động lại Nginx
systemctl restart nginx

# Kiểm tra trạng thái
systemctl status nginx
```

---

## Bước 8: Cấu hình Firewall

```bash
# Cài đặt UFW nếu chưa có
apt install -y ufw

# Cấu hình firewall
ufw allow ssh
ufw allow http
ufw allow https
ufw enable

# Kiểm tra trạng thái
ufw status
```

---

## Bước 9: Tạo Systemd Service cho ứng dụng

```bash
# Tạo service file
cat > /etc/systemd/system/pcmsystem.service << 'EOF'
[Unit]
Description=PCMSystem - PCM Management System
After=network.target

[Service]
WorkingDirectory=/var/www/pcmsystem
ExecStart=/usr/bin/dotnet /var/www/pcmsystem/PCMSystem.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DotNetShowBanner=false

[Install]
WantedBy=multi-user.target
EOF

# Reload systemd
systemctl daemon-reload

# Kích hoạt service
systemctl enable pcmsystem.service

# Khởi động ứng dụng
systemctl start pcmsystem.service

# Kiểm tra trạng thái
systemctl status pcmsystem.service

# Xem logs
journalctl -u pcmsystem.service -f
```

---

## Bước 10: Kiểm tra và troubleshooting

```bash
# Kiểm tra ứng dụng có đang chạy không
curl http://localhost:5000

# Xem logs chi tiết
journalctl -u pcmsystem.service -n 100

# Restart ứng dụng
systemctl restart pcmsystem.service

# Stop ứng dụng
systemctl stop pcmsystem.service
```

---

## Bước 11: (Tùy chọn) Cấu hình SSL/HTTPS miễn phí

```bash
# Cài đặt SSL với Let's Encrypt
certbot --nginx -d daovietem.duckdns.org

# Chọn option 2 (Redirect HTTP to HTTPS)
```

Sau khi cài đặt SSL, ứng dụng sẽ truy cập được qua:
- https://daovietem.duckdns.org

---

## Các lệnh hữu ích

| Lệnh | Mô tả |
|------|-------|
| `systemctl status pcmsystem` | Xem trạng thái ứng dụng |
| `systemctl restart pcmsystem` | Khởi động lại ứng dụng |
| `journalctl -u pcmsystem -f` | Xem logs real-time |
| `systemctl stop pcmsystem` | Dừng ứng dụng |
| `systemctl start pcmsystem` | Bắt đầu ứng dụng |
| `nginx -t` | Kiểm tra cấu hình Nginx |
| `systemctl restart nginx` | Khởi động lại Nginx |
| `cat /var/log/nginx/pcmsystem_error.log` | Xem lỗi Nginx |

---

## Cập nhật ứng dụng (khi có thay đổi code)

```bash
# Trên máy local - build lại
dotnet publish -c Release -r linux-x64 --self-contained false -o C:\Users\Admin\Desktop\BAIKIEMTRA2\kiemtra2\PCMSystem\publish
tar -czvf C:\Users\Admin\Desktop\BAIKIEMTRA2\kiemtra2\PCMSystem\pcmsystem.tar.gz -C C:\Users\Admin\Desktop\BAIKIEMTRA2\kiemtra2\PCMSystem\publish .

# Upload lên VPS (sử dụng SCP)
scp C:\Users\Admin\Desktop\BAIKIEMTRA2\kiemtra2\PCMSystem\pcmsystem.tar.gz root@103.77.172.155:/root/

# Trên VPS
systemctl stop pcmsystem
cd /var/www/pcmsystem
rm -rf *
tar -xzvf /root/pcmsystem.tar.gz -C /var/www/pcmsystem/
chown -R www-data:www-data /var/www/pcmsystem
systemctl start pcmsystem
```

---

## Troubleshooting thường gặp

1. **Lỗi kết nối database**:
   ```bash
   # Kiểm tra file database tồn tại
   ls -la /var/www/pcmsystem/data/
   ```

2. **Lỗi port**:
   ```bash
   # Kiểm tra port đang sử dụng
   netstat -tlnp | grep 5000
   ```

3. **Lỗi permissions**:
   ```bash
   # Sửa quyền
   chown -R www-data:www-data /var/www/pcmsystem
   chmod -R 755 /var/www/pcmsystem
   ```

4. **Kiểm tra logs chi tiết**:
   ```bash
   journalctl -u pcmsystem.service --no-pager -n 200
   ```
