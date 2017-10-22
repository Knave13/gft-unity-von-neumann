var express = require('express');
var router = express.Router();
var data = {
    galaxies: [{
            name: "Blue",
            id: 1
        },
        {
            name: "Blue",
            id: 1
        }
    ]
}

/* GET users listing. */
router.get('/', function(req, res, next) {
    res.send(data);
});

module.exports = router;