# Silent guardian
Dashboard for network endpoints (PCs, servers,...). Ping visualization. Customisable click action. Easy to use.

# settings.json example
Json like this must be inside program´s directory. Rows and Columns set to 0 will generate them automatically. Ping timer is in milliseconds.

    {
      "Rows": "0",
      "Columns": "0",
      "GroupRows": "0",
      "GroupColumns": "0",
      "PingTimer": "15000",

      "Group": [
        {
          "Name": "Work",
          "Endpoint": [
            {
              "name": "PC1",
              "description": "",
              "location": "172.23.121.26",
              "leftaction": "mstsc /v:{location}"
            },
            {
              "name": "PC2",
              "description": "",
              "location": "172.23.121.27"
            },
            {
              "name": "Server",
              "description": "",
              "location": "172.23.12.60"
            }
          ]
        },
        {
          "Name": "Google",
          "Endpoint": [
            {
              "name": "Gmail",
              "description": "",
              "location": "mail.google.com"
            },
            {
              "name": "YouTube",
              "description": "",
              "location": "youtube.com"
            },
            {
              "name": "Drive",
              "description": "",
              "location": "drive.google.com"
            }
          ]
        },
        {
          "Name": "My network",
          "Endpoint": [
            {
              "name": "Router",
              "description": "",
              "location": "192.168.0.1"
            },
            {
              "name": "Me",
              "description": "",
              "location": "127.0.0.1"
            },
            {
              "name": "Computer 2",
              "description": "",
              "location": "192.168.0.57"
            }
          ]
        }
      ]
    }
