print('COW: Loading LUA Backend...')
Json = require('packages/'..GetPackageName()..'/server/library/json');
-- START LUA API --

function OnTest(data)
    return 'Test'
end






-- END LUA API --
print('COW: LUA Backend loaded!')
print('COW: Trigger Wrapper to finish!')
ExecuteNET('finish-wrapper', '');