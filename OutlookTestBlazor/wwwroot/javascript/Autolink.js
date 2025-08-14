// 📁 OutlookAddinSideloadWeb/wwwroot/js/auth/autolink.js

Office.onReady(() => {
    if (Office.context.mailbox && Office.context.mailbox.item) {
        const item = Office.context.mailbox.item;

        item.from.getAsync(r => {
            const sender = r.value.emailAddress;

            item.body.getAsync("text", b => {
                const content = b.value;

                fetch("/api/autolink", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ sender, content })
                })
                    .then(res => res.json())
                    .then(data => {
                        if (data.matchFound) {
                            alert(`Linked to: ${data.record.Type} — ${data.record.Name}`);
                        } else if (confirm("No match. Create new?")) {
                            fetch("/api/autolink/createrecord", {
                                method: "POST",
                                headers: { "Content-Type": "application/json" },
                                body: JSON.stringify({ sender, content })
                            })
                                .then(r => r.json())
                                .then(d => alert(`Created: ${d.record.Type} — ${d.record.Name}`));
                        }
                    });
            });
        });
    }
});
