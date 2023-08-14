import datetime
import json
import math
from io import BytesIO
from PIL import Image
import numpy as np
import requests
from flask import Flask, jsonify, request
import asyncio
import base64


def getDataArchery(winRate, TBwinRate):
    url = "https://api.worldarchery.org/v3/ATHLETEBIOGRAPHY/?Id=13418&Detailed=1"
    response = requests.get(url=url)

    jsonDict = json.loads(response.text)
    # name = jsonDict['items'][0]['GName'] + jsonDict['items'][0]['FName']

    for i in range(2018, 2024):
        TBwinRate.append(math.ceil(jsonDict['items'][0]['Stats'][str(i)][0]['TBWinPercentage']))
        winRate.append(math.ceil(jsonDict['items'][0]['Stats'][str(i)][0]['MatchWinPercentage']))




def getDataArray(array1, array2):
    array = "a:"
    sampleArr = np.array([array1, array2])

    for row in sampleArr:
        for element in row:
            array += str(element) + ','
        array = array[:-1]
        array += '|'

    return array[:-1]


def getLabelArray(array1, array2):
    array = ""
    iterated_Arr = np.array([array2 + array1])

    for row in iterated_Arr:
        for x in row:
            array += str(x) + '|'

    return array


def getGraph(name, winRate, TBwinRate):
    getDataArchery(TBwinRate, winRate)
    array_Str = getDataArray(TBwinRate, winRate)

    url = "https://image-charts.com/chart"
    payload = {
        'cht': 'bvg',
        'chbr': '10',
        'chco': '008080,27c9c4',
        'chdlp': 'r',
        'chd': array_Str,
        'chds': '0,100',
        'chdl': 'WinRate|Tiebreak WinRate',
        'chl': getLabelArray(winRate, TBwinRate),
        'chxl': '0:|2018|2019|2020|2021|2022|2023',
        'chs': '950x950',
        'chtt': name,
        'chxt': "x,y"

    }
    response = requests.post(url, payload)
    print(name)
    base64_bytes = base64.b64encode(response.content)
    base64_string = base64_bytes.decode("ascii")

    return base64_string


app = Flask(__name__)


@app.route("/")
def startup():
    return "Welcome to The System"


@app.route("/getTarihSaat")
def getDate():
    date = datetime.datetime.now()
    dateNow = str(date.strftime("%Y-%m-%d %H:%M:%S"))

    message = {
        'Date': dateNow,
    }

    return message


@app.route("/createResults", methods=['POST'])
def createChart():
    data = json.loads(request.data)
    winrate = []
    TBwinrate = []

    imageBytes = getGraph(list(data.values())[0], winrate, TBwinrate)

    response = {
        'imageBytes': '{0}'.format(imageBytes)
    }

    return response, 201


if __name__ == '__main__':
    app.run(debug=True)
