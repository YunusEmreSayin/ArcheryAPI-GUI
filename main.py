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
from aiohttp import web


def getDataArchery(winRate, TBwinRate):
    """ Connecting Url for getting data"""
    url = "https://api.worldarchery.org/v3/ATHLETEBIOGRAPHY/?Id=13418&Detailed=1"
    response = requests.get(url=url)

    """ Defining data result as a dictionary"""
    jsonDict = json.loads(response.text)
    # name = jsonDict['items'][0]['GName'] + jsonDict['items'][0]['FName']

    """Getting target datas in target range"""
    for i in range(2018, 2024):
        TBwinRate.append(math.ceil(jsonDict['items'][0]['Stats'][str(i)][0]['TBWinPercentage']))
        winRate.append(math.ceil(jsonDict['items'][0]['Stats'][str(i)][0]['MatchWinPercentage']))


""" Creating Needed data for Creating Chart-Data"""


def getDataArray(array1, array2):
    sampleString = "a:"
    sampleArr = np.array([array1, array2])

    for row in sampleArr:
        for element in row:
            sampleString += str(element) + ','
        """ Removing the unnecessary last char"""
        sampleString = sampleString[:-1]
        sampleString += '|'

    return sampleString[:-1]


""" Creating Needed data for Creating Chart-Labels"""


def getLabelArray(array1, array2):
    dataString = ""
    iterated_Arr = np.array([array2 + array1])

    for row in iterated_Arr:
        for x in row:
            dataString += str(x) + '|'

    return dataString


"""Creating chart returning it as base64 String"""


def getGraph(name):
    """Defining Arrays for storing data"""

    winrate = []
    TBwinrate = []

    """Filling arrays with needed datas"""
    getDataArchery(TBwinrate, winrate)
    array_Str = getDataArray(TBwinrate, winrate)

    """Defining the url for Creating Chart"""
    url = "https://image-charts.com/chart"

    """Creating parameters for Chart as payload """
    payload = {
        'cht': 'bvg',
        'chbr': '10',
        'chco': '008080,27c9c4',
        'chdlp': 'r',
        'chd': array_Str,
        'chds': '0,100',
        'chdl': 'WinRate|Tiebreak WinRate',
        'chl': getLabelArray(winrate, TBwinrate),
        'chxl': '0:|2018|2019|2020|2021|2022|2023',
        'chs': '950x950',
        'chtt': name,
        'chxt': "x,y"

    }
    """Using post method for creating Chart"""
    response = requests.post(url, payload)

    """Parsing PNG file to base64 string"""
    base64_bytes = base64.b64encode(response.content)
    base64_string = base64_bytes.decode("ascii")

    return base64_string


"""defining flask application"""
app = Flask(__name__)

"""Home Page"""


@app.route("/")
def startup():
    return "Welcome to The System"


"""Page using for getting realtime DateTime data"""


@app.route("/getTarihSaat")
def getDate():
    """Getting realtime DateTime data and formatting like yyyy-mm-dd HH:MM:SS"""
    dateNow = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")

    """Defining dictionary for result json file"""
    message = {
        'Date': dateNow,
    }

    return message


"""Page using for creating Chart"""


@app.route("/createResults", methods=['POST'])
def createChart():
    """Getting payload as dictionary"""
    data = json.loads(request.data)

    """Getting chart as base64 string"""
    imageBytes = getGraph(list(data.values())[0])

    """ Defining the response dictionary for result json"""
    response = {
        'imageBytes': '{0}'.format(imageBytes)
    }

    return response, 201


if __name__ == '__main__':
    app.run(debug=True)
