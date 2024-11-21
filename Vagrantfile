Vagrant.configure("2") do |config|

  # Виртуальная машина Ubuntu
  config.vm.define "ubuntu" do |ubuntu|
    ubuntu.vm.box = "ubuntu/jammy64"
    ubuntu.vm.network "public_network"
    ubuntu.vm.provider "virtualbox" do |vb|
      vb.memory = "8192"
      vb.cpus = 4
    end

    # Настройка для установки .NET на Ubuntu
    ubuntu.vm.provision "shell", inline: <<-SHELL
      # Обновляем систему
      sudo apt-get update
      sudo apt-get install -y wget apt-transport-https
      # Устанавливаем GPG ключ Microsoft
      wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb
      sudo dpkg -i packages-microsoft-prod.deb
      # Устанавливаем .NET SDK
      sudo apt-get update
      sudo apt-get install -y dotnet-sdk-8.0
      # Проверяем установку
      dotnet version

      # Настройка NuGet источника для приватного репозитория BaGet
      dotnet nuget add source http://192.168.56.1:5000/v3/index.json --name "BaGet"

      # Устанавливаем инструмент Lab4
      dotnet tool install --global Lab4 --version 1.0.0

      # Проверяем установку
      Lab4 version
    SHELL

    # Синхронизированная папка для виртуальной машины Linux
    ubuntu.vm.synced_folder ".", "/home/vagrant/project"
  end

  # Виртуальная машина Windows
  config.vm.define "windows" do |windows|
    windows.vm.box = "gusztavvargadr/windows-10"
    windows.vm.network "public_network"
    windows.vm.provider "virtualbox" do |vb|
      vb.memory = "8192"
      vb.cpus = 4
    end

    # Настройка для установки Chocolatey и .NET SDK
    windows.vm.provision "shell", inline: <<-SHELL
      # Устанавливаем Chocolatey
      Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12; 
      iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
      # Устанавливаем .NET SDK 8.0 с помощью Chocolatey
      choco install dotnet-8.0-sdk -y
      # Проверяем установку
      & "C:\\ProgramData\\chocolatey\\bin\\dotnet.exe" --version

      # Настройка NuGet источника для приватного репозитория BaGet
      dotnet nuget add source http://192.168.0.101:5000/v3/index.json --name "BaGet"

      # Устанавливаем инструмент Lab4
      dotnet tool install --global Lab4 --version 1.0.3

      # Проверяем установку
      Lab4 version
    SHELL

    # Синхронизированная папка для виртуальной машины Windows
    windows.vm.synced_folder ".", "C:/project"
  end
end