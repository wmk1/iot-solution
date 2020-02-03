const express = require('express');
const http = require('http');
const WebSocket = require('ws');
const path = require('path');
const EventHubReader = require('./scripts/event-hub-reader.js');
const axios = require('axios'); 

const iotHubConnectionString = 'HostName=cdv-iot-hub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=/NA9s8mNeIH7FrF57er2fMZBaKB1EmGJjxi0FUfBS+U=';
const eventHubConsumerGroup = 'cdv-iot-consumer-group';

process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = 0

// Redirect requests to the public subdirectory to the root
const app = express();
app.use(express.static(path.join(__dirname, 'public')));
app.use((req, res /* , next */) => {
  res.redirect('/');
});

const server = http.createServer(app);
const wss = new WebSocket.Server({ server });

wss.broadcast = (data) => {

    const dataParsed = JSON.parse(data);
  wss.clients.forEach((client) => {
    if (client.readyState === WebSocket.OPEN) {
      try {
          result = {
              messageId: dataParsed.IotData.messageId,
              deviceId: dataParsed.IotData.deviceId,
              temperature: dataParsed.IotData.temperature,
              humidity: dataParsed.IotData.humidity,
              messageDate: dataParsed.MessageDate
          }

          axios.post('https://localhost:44344/Home/pushdata', result).then((res) => {
              console.log(`statusCode: ${res.statusCode}`);
              console.log(res);
          }).catch((error) => {
              console.error(error)
          })
        client.send(data);
      } catch (e) {
        console.error(e);
      }
    }
  });
};

server.listen(process.env.PORT || '3000', () => {
  console.log('Listening on %d.', server.address().port);
  console.log('Env variables: ', process.env.createFromIotHubConnectionString, process.env.PWD);
  console.log(JSON.stringify(eventHubReader));
});

const eventHubReader = new EventHubReader(iotHubConnectionString, eventHubConsumerGroup);

(async () => {
  await eventHubReader.startReadMessage((message, date, deviceId) => {
    try {
      const payload = {
        IotData: message,
        MessageDate: date || Date.now().toISOString(),
        DeviceId: deviceId,
      };

      wss.broadcast(JSON.stringify(payload));
    } catch (err) {
      console.error('Error broadcasting: [%s] from [%s].', err, message);
    }
  });
})().catch();
