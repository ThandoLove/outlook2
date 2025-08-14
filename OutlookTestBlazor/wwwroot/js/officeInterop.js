// officeInterop.js - exposes officeInterop.getMessageDetails()
window.officeInterop = (function () {
  return {
    async getMessageDetails() {
      return new Promise(async function (resolve) {
        try {
          if (typeof Office === 'undefined') {
            resolve({ error: 'Office.js not available' });
            return;
          }
          await Office.onReady();
          const item = Office.context && Office.context.mailbox && Office.context.mailbox.item;
          if (!item) {
            resolve({ error: 'No mailbox item' });
            return;
          }
          const details = { subject: '', from: '', body: '' };
          try { details.subject = item.subject || ''; } catch (e) { details.subject = ''; }
          try {
            if (item.from) details.from = (item.from.displayName || '') + ' <' + (item.from.emailAddress || '') + '>';
            else if (item.sender) details.from = (item.sender.displayName || '') + ' <' + (item.sender.emailAddress || '') + '>';
          } catch (e) { details.from = ''; }
          try {
            if (item.body && item.body.getAsync) {
              item.body.getAsync("text", function (result) {
                if (result.status === Office.AsyncResultStatus.Succeeded) {
                  details.body = result.value || '';
                }
                resolve(details);
              });
            } else if (item.getBodyAsync) {
              item.getBodyAsync("text", function (result) {
                if (result.status === Office.AsyncResultStatus.Succeeded) {
                  details.body = result.value || '';
                }
                resolve(details);
              });
            } else {
              details.body = '';
              resolve(details);
            }
          } catch (e) {
            details.body = '';
            resolve(details);
          }
        } catch (ex) {
          resolve({ error: ex.message || String(ex) });
        }
      });
    }
  };
})();
